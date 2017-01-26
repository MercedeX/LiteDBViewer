using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using LiteDB;
using LiteDB_V6;

namespace LiteDBViewer
{
    public enum NodeType { Unknown=0, Database, Table, FileStorage };
    public struct NodeInfo
    {
        public readonly NodeType type;
        public readonly int databaseId;
        public readonly string collection;

        public NodeInfo(int dbId)
        {
            type = NodeType.Database;
            databaseId = dbId;
            collection = string.Empty;
        }
        public NodeInfo(int dbId, string name)
        {
            type = NodeType.Table;
            databaseId = dbId;
            collection = name;
        }
        public NodeInfo(NodeType typ = NodeType.Unknown)
        {
            type = typ;
            databaseId = -1;
            collection = string.Empty;
        }
    }

    internal partial class MainForm : Form
    {
        private const int CollectionsResultLimit = 100;
        private bool _encrypted;
        private string _fileName;

        private readonly List<Tuple<int, DatabaseWrapper>>
            _databases = new List<Tuple<int, DatabaseWrapper>>();

        private readonly Dictionary<BsonDocument, LiteFileInfo> _fileStorageBinding =
            new Dictionary<BsonDocument, LiteFileInfo>();

        //private LiteDatabase _db;

        public MainForm()
        {
            //OpenDatabase(fileName, password);
            InitializeComponent();
        }

        //private void OpenDatabase(string filename, string password)
        //{
        //    _encrypted = !string.IsNullOrWhiteSpace(password);
        //    _fileName = Path.GetFullPath(filename);
        //    _db = new LiteDatabase(_encrypted ? $"password=\"{password}\";filename=\"{_fileName}\"" : _fileName);

        //    //txt_filename.Text = _fileName + (_encrypted ? " [ENCRYPTED]" : string.Empty);

        //    CategoryView.Nodes.Clear();
        //    var node = new TreeNode("Tables");
        //    node.Tag = null;
        //    foreach (var collection in _db.GetCollectionNames())
        //    {
        //        if (!collection.Equals("_chunks") && !collection.Equals("_files"))
        //        {
        //            var child = new TreeNode(collection);
        //            child.Tag = collection;
        //            node.Nodes.Add(child);
        //        }
        //    }
        //    CategoryView.Nodes.Add(node);

        
        //    CategoryView.Nodes.Add(new TreeNode("FileStorage"));
        //    //lb_Collections.Items.Add("[FILESTORAGE]");

        //    //lb_Collections.Items.Clear();
        //    //foreach (var item in _db.GetCollectionNames())
        //    //{
        //    //    lb_Collections.Items.Add(item);
        //    //} 
        //}

        //private void MainForm_Load(object sender, EventArgs e)
        //{
        //    Text = Text.Replace("{APPVERSION}", Assembly.GetExecutingAssembly().GetName().Version.ToString())
        //        .Replace("{DBVERSION}", Assembly.GetAssembly(typeof(LiteDatabase)).GetName().Version.ToString());
        //    Activate();
        //}

        //private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    _fileStorageBinding.Clear();
        //    if (lb_Collections.SelectedItem != null && !lb_Collections.SelectedItem.Equals("[QUERY]") &&
        //        !lb_Collections.SelectedItem.Equals("[FILESTORAGE]"))
        //    {
        //        FillDataGridView(_db.GetCollection(lb_Collections.SelectedItem.ToString())
        //            .Find(Query.All(), 0, CollectionsResultLimit));
        //        txt_query.Text = $"db.{lb_Collections.SelectedItem}.find limit {CollectionsResultLimit}";
        //    }
        //    else if (lb_Collections.SelectedItem?.Equals("[FILESTORAGE]") == true)
        //    {
        //        foreach (var fileInfo in _db.FileStorage.FindAll())
        //        {
        //            _fileStorageBinding.Add(fileInfo.AsDocument, fileInfo);
        //        }
        //        FillDataGridView(_fileStorageBinding.Keys.ToArray());
        //        txt_query.Text = @"fs.find";
        //    }
        //}

        public void FillDataGridView(IEnumerable<BsonDocument> documents)
        {
            dataGridView.DataSource = null;
            if (documents != null)
            {
                var dt = new LiteDataTable(documents.ToString());
                foreach (var doc in documents)
                {
                    var dr = dt.NewRow() as LiteDataRow;
                    if (dr != null)
                    {
                        dr.UnderlyingValue = doc;
                        foreach (var property in doc.RawValue)
                        {
                            if (!property.Value.IsMaxValue && !property.Value.IsMinValue)
                            {
                                if (!dt.Columns.Contains(property.Key))
                                {
                                    dt.Columns.Add(new DataColumn(property.Key, typeof (string)));
                                }
                                switch (property.Value.Type)
                                {
                                    case BsonType.Null:
                                        dr[property.Key] = "[NULL]";
                                        break;
                                    case BsonType.Document:
                                        dr[property.Key] = property.Value.AsDocument.RawValue.ContainsKey("_type")
                                            ? $"[OBJECT: {property.Value.AsDocument.RawValue["_type"]}]"
                                            : "[OBJECT]";
                                        break;
                                    case BsonType.Array:
                                        dr[property.Key] = $"[ARRAY({property.Value.AsArray.Count})]";
                                        break;
                                    case BsonType.Binary:
                                        dr[property.Key] = $"[BINARY({property.Value.AsBinary.Length})]";
                                        break;
                                    case BsonType.DateTime:
                                        dr[property.Key] = property.Value.AsDateTime.ToString("yyyy-MM-ddd HH:mm:ss.fff");
                                        break;
                                    default:
                                        dr[property.Key] = property.Value.ToString();
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
                dataGridView.DataSource = dt;
            }
        }

        private void dataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right || (e.Button == MouseButtons.Left && e.Clicks > 1))
            {
                var currentMouseOver = dataGridView.HitTest(e.X, e.Y);
                if (currentMouseOver.RowIndex >= 0)
                {
                    var dataRowValue =
                        ((dataGridView.Rows[currentMouseOver.RowIndex].DataBoundItem as DataRowView)?.Row as
                            LiteDataRow)?.UnderlyingValue;
                    if (dataRowValue != null)
                    {
                        var m = new ContextMenu();
                        m.MenuItems.Add(new MenuItem("View Row as Object",
                            (o, args) => new DocumentViewForm(dataRowValue.AsDocument).ShowDialog(this)));
                        if (_fileStorageBinding.ContainsKey(dataRowValue))
                        {
                            m.MenuItems.Add("-");
                            m.MenuItems.Add(new MenuItem("View Stored Binary Data",
                                (o, args) =>
                                {
                                    var bytes = new byte[_fileStorageBinding[dataRowValue].Length];
                                    _fileStorageBinding[dataRowValue].OpenRead().Read(bytes, 0, bytes.Length);
                                    new BinaryViewForm(bytes).ShowDialog(this);
                                }));
                            m.MenuItems.Add(new MenuItem("Save Stored Data to File",
                                (o, args) =>
                                {
                                    try
                                    {
                                        var extention =
                                            Path.GetExtension(_fileStorageBinding[dataRowValue].Filename)?.ToLower() ??
                                            ".*";
                                        var sfd = new SaveFileDialog
                                        {
                                            RestoreDirectory = true,
                                            Title = @"Save data to file",
                                            Filter = $"'{extention}' File|*{extention}|All Files|*.*",
                                            FileName = _fileStorageBinding[dataRowValue].Filename
                                        };
                                        if (sfd.ShowDialog() != DialogResult.OK)
                                            return;
                                        _fileStorageBinding[dataRowValue].SaveAs(sfd.FileName);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message, @"Saving Data", MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                                    }
                                }));
                        }
                        if (currentMouseOver.ColumnIndex >= 0)
                        {
                            var cell = dataRowValue[dataGridView.Columns[currentMouseOver.ColumnIndex].Name];
                            switch (cell.Type)
                            {
                                case BsonType.String:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View String",
                                        (o, args) => new StringViewForm(cell.AsString).ShowDialog(this)));
                                    break;
                                case BsonType.Document:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Object",
                                        (o, args) => new DocumentViewForm(cell.AsDocument).ShowDialog(this)));
                                    break;
                                case BsonType.Array:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Array",
                                        (o, args) => new ArrayViewForm(cell.AsArray).ShowDialog(this)));
                                    break;
                                case BsonType.Binary:
                                    m.MenuItems.Add("-");
                                    m.MenuItems.Add(new MenuItem("View Binary",
                                        (o, args) => new BinaryViewForm(cell.AsBinary).ShowDialog(this)));
                                    break;
                            }
                        }
                        m.Show(dataGridView, new Point(e.X, e.Y));
                    }
                }
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                RunQuery(txt_query.Text);
                //if (!lb_Collections.Items.Contains("[QUERY]"))
                //{
                //    lb_Collections.Items.Add("[QUERY]");
                //}
                //lb_Collections.SelectedItem = "[QUERY]";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
                //if (_db != null)
                //{
                //    _db.Dispose();
                //    _db = null;
                //}
            }
            base.Dispose(disposing);
        }

        private void RunQuery(string query)
        {
            try
            {
                //txt_query.Text = query;
                //FillDataGridView(null);
                //var result = _db.Run(query);
                //var rows = new List<BsonDocument>();
                //if (result.IsArray)
                //{
                //    rows.AddRange(
                //        result.AsArray.Select(
                //            item => item.IsDocument ? item.AsDocument : new BsonDocument().Add("RESULT", result)));
                //}
                //else
                //{
                //    rows.Add(new BsonDocument().Add("RESULT", result));
                //}
                //FillDataGridView(rows);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Bad Query", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Info_Click(object sender, EventArgs e)
        {
            var infos = new Dictionary<string, BsonValue>
            {
                {"DatabaseVersion", new BsonValue((int) 0 )},
                {"FileName", new BsonValue(_fileName)},
                {"Encrypted", new BsonValue(_encrypted)}
            };
            //foreach (var collectionName in _db.GetCollectionNames())
            //{
            //    //infos.Add($"[{collectionName}] Stats", _db.GetCollection(collectionName)..Run($"db.{collectionName}.stats"));
            //}
            new DocumentViewForm(new BsonDocument(infos)).ShowDialog();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Title = @"Dump Database data to file",
                Filter = @"Dump file|*.dmp"
            };
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                using (var writer = File.CreateText(sfd.FileName))
                {
                    var mapper = new BsonMapper().UseCamelCase();
                    var db = GetCurrentDatabase();
                    foreach (var col in db.Tables)
                    {
                        writer.WriteLine("-- Collection '{0}'", col.Name);
                        foreach (var index in col.GetIndexes().Where(x => x.Field != "_id"))
                        {
                            //writer.WriteLine("db.{0}.ensureIndex {1} {2}", col.Name, index.Field,
                            //    JsonSerializer.Serialize(mapper.ToDocument(index.)));
                        }
                        foreach (var doc in col.Find(Query.All()))
                        {
                            writer.WriteLine("db.{0}.insert {1}", col.Name, JsonSerializer.Serialize(doc));
                        }
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Dumping Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DatabaseWrapper GetCurrentDatabase()
        {
            DatabaseWrapper ret = null;
            if(CategoryView.SelectedNode.Tag != null)
            {
                var info = (NodeInfo)CategoryView.SelectedNode.Tag;
                if(info.type!= NodeType.Unknown)
                    ret = _databases.FirstOrDefault(x => x.Item1 == info.databaseId).Item2;
            }
            return ret;
        }

        private void FileExit_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            using(var ini = new IniFile(Konstants.IniFilePath))
            {
                var tmp = ini.Get<string>("LastLocation");
                if (Directory.Exists(tmp))
                    path = tmp;
                else
                    ini.Set("LastLocation", path);
            }

            var ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = true,
                RestoreDirectory = true,
                Title =
                    $@"Open LiteDB Database File - LiteDB Viewer v{
                        Assembly.GetExecutingAssembly().GetName().Version}",
                Filter = $"LiteDB v{Assembly.GetAssembly(typeof(LiteDatabase)).GetName().Version.Major} Files|*.*",
                InitialDirectory = path
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //Save last path
                using (var ini = new IniFile(Konstants.IniFilePath))
                    ini.Set("LastLocation", Path.GetDirectoryName(ofd.FileName));

                var wrapper = new DatabaseWrapper(ofd.FileName);
                _databases.Add(new Tuple<int, DatabaseWrapper>(_databases.Count + 1, wrapper));

                Refresh();
            }
              //  OpenDatabase(ofd.FileName, string.Empty);
        }

        private void Node_Selected(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                var info = (NodeInfo) e.Node.Tag;
                var db = _databases.FirstOrDefault(x => x.Item1 == info.databaseId).Item2;

                switch (info.type)
                {
                    case NodeType.Table:
                        FillDataGridView(db
                            .Tables
                            .FirstOrDefault(x => string.Compare(x.Name, info.collection, true) == 0)
                            .Find(Query.All(), limit: 100)
                            );
                        break;
                    case NodeType.FileStorage:
                        FillDataGridView(db
                            .Tables
                            .FirstOrDefault(x => string.Compare(x.Name, "FileStorage", true) == 0)
                            .Find(Query.All(), limit: 100)
                            );
                        break;
                }
            }
        }

        public override void Refresh()
        {
            base.Refresh();

            CategoryView.Nodes.Clear();
            var databasesNode = new TreeNode("Databases", 0, 0);
            foreach(var db in _databases)
            {
                var node = GetDatabaseNode(db.Item1, db.Item2);
                databasesNode.Nodes.Add(node);
            }
            CategoryView.Nodes.Add(databasesNode);
            CategoryView.Select();
        }

        private TreeNode GetDatabaseNode(int id, DatabaseWrapper db)
        {
            var root = new TreeNode(db.Properties.fileName);
            root.Tag = new NodeInfo(id);

            foreach (var tb in db.Tables)
            {
                var tNode = new TreeNode(tb.Name);
                tNode.Tag = new NodeInfo(id, tb.Name);
                root.Nodes.Add(tNode);
            }

            return root;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;

namespace LiteDBViewer
{
    public struct DBProperties
    {
        public readonly string fileName;
        public readonly string path;
        public readonly long sizeInBytes;
        public readonly int version;

        public DBProperties(string fileWithPath, int ver=0)
        {
            fileName = Path.GetFileName(fileWithPath);
            path = fileWithPath.Replace(fileName, string.Empty);
            sizeInBytes=(new FileInfo(fileWithPath)).Length;
            version = ver;
        }
    }
    class DatabaseWrapper
    {
        public DBProperties Properties { get; }
        public DatabaseWrapper(string fileName)
        {
            Contract.Requires(fileName != null, $"file name cannot be null");

            Properties = new DBProperties(fileName, 0);
            var db = new LiteDatabase(fileName);

            Contract.Assert(db != null);

            var tables = new List<LiteCollection<BsonDocument>>();
            foreach (var name in db.GetCollectionNames())
            {
                var col = db.GetCollection(name);
                tables.Add(col);
            }
            Tables = tables;
        }

        public IEnumerable<LiteCollection<BsonDocument>> Tables { get; }


    }
}

using System;
using ProtoBuf;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace conf.Building
{
    [ProtoContract]
    public partial class Building
    {
        [ProtoMember(1)] public System.Int32 Id { get; private set; }
        [ProtoMember(2)] public System.String Name { get; private set; }
        [ProtoMember(3)] public System.Double Price { get; private set; }
        [ProtoMember(4)] public System.String PrefabPath { get; private set; }
    }

    [ProtoContract]
    public partial class BuildingMgr
    {
        [ProtoMember(1)] private Dictionary<System.Int32, Building> _dict = new Dictionary<System.Int32, Building>();
        public IReadOnlyDictionary<System.Int32, Building> Dict => _dict;
        public Building Get(System.Int32 id) => _dict.TryGetValue(id, out var t) ? t : null;
        private static BuildingMgr _instance = null;
        public static BuildingMgr GetInstance() => _instance;
        private static FileInfo _lastReadFile = null;

        protected BuildingMgr()
        {
        }

        public static void InitInstance(FileInfo file)
        {
            _instance = null;
            using (FileStream fs = file.Open(FileMode.Open, FileAccess.Read))
            {
                _instance = Serializer.DeserializeWithLengthPrefix<BuildingMgr>(fs, PrefixStyle.Fixed32);
                Debug.Assert(_instance != null, "Load Config Building failed at " + file.FullName);
                _lastReadFile = file;
            }
        }

        public static void Reload()
        {
            if (_lastReadFile == null) return;
            InitInstance(_lastReadFile);
        }

        public static void Save(FileInfo file)
        {
            using (FileStream fs = file.Open(FileMode.OpenOrCreate, FileAccess.Write))
            {
                Serializer.SerializeWithLengthPrefix(fs, _instance, PrefixStyle.Fixed32);
            }
        }

        public static void AppendData(System.Int32 id, Building d)
        {
            if (_instance == null)
                _instance = new BuildingMgr();
            Debug.Assert(_instance._dict.ContainsKey(id) == false, "Append Same Building id = " + id);
            _instance._dict.Add(id, d);
        }
    }
}
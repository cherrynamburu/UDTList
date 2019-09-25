using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;



[Serializable]
[SqlUserDefinedType(Format.UserDefined, IsByteOrdered = true, MaxByteSize = -1)]
public class UDTList : INullable, IBinarySerialize
{

    private const int MAX_LENGTH = 15;
    private int _maxSize = MAX_LENGTH;


    public bool IsNull { get; set; }
    public int Count { get; set; }
    public int MaxSize
    {
        get
        {
            return _maxSize;
        }
        set
        {
            if (value < MAX_LENGTH)
                _maxSize = value;
            else
                _maxSize = MAX_LENGTH;
        }
    }
    public List<Employee> List { get; set; }
    

    public static UDTList Null
    {
        get
        {
            var udtlist = new UDTList
            {
                IsNull = true
            };
            return udtlist;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static UDTList Parse(SqlString sqlString)
    {
        if (sqlString.IsNull)
            return Null;

        var udtlist = new UDTList();
        udtlist.List = udtlist.GetEmployeeList(sqlString.Value);
        return udtlist;
    }

    private List<Employee> GetEmployeeList(string text)
    {
        string[] delimiters = new string[4] { " # ", " #", "# ", "#" };
        List<Employee> list = new List<Employee>();

        string[] words = (text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
        Count = words.Length > MAX_LENGTH ? MAX_LENGTH : words.Length;
        for (int i = 0; i < Count; i++)
        {
            list.Add(Employee.Parse(words[i]));
        }
        return list;
    }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";

        return string.Join(",", List);
    }

    public string ToXML()
    {
        var serializer = new XmlSerializer(typeof(UDTList));
        var stringWriter = new StringWriter();
        serializer.Serialize(stringWriter, this);
        return stringWriter.ToString();
    }

    public void Read(BinaryReader r)
    {
        var _byteLength = r.ReadInt32();
        var byteArray = r.ReadBytes(_byteLength);
        var formatter = new BinaryFormatter();
        var stream = new MemoryStream(byteArray);
        List = (List<Employee>)formatter.Deserialize(stream);
        Count = r.ReadInt32();
        IsNull = r.ReadBoolean();
    }

    public void Write(BinaryWriter w)
    {
        var formatter = new BinaryFormatter();
        var stream = new MemoryStream();
        formatter.Serialize(stream, List);
        var byteArray = stream.ToArray();
        var _byteLength = byteArray.Length;
        w.Write(_byteLength);
        w.Write(byteArray);
        w.Write(Count);
        w.Write(IsNull);
    }

    public UDTList AddEmployee(SqlString sqlString)
    {
        var employee = Employee.Parse(sqlString);

        if (Count <= MaxSize)
        {
            List.Add(employee);
            Count += 1;
        }
        return this;
    }

    public UDTList RemoveEmployee()
    {
        if (Count>0)
        {
            List.RemoveAt(Count-1);
            Count--;
        }     
        return this;
    }
}

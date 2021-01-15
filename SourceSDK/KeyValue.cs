using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SourceSDK
{
    public class KeyValue
    {
        private List<KeyValue> children = null;
        private Dictionary<string, List<KeyValue>> childrenIndex = null;

        private string key = string.Empty;
        private string value = null;
        private string comment = string.Empty;

        public static bool KeyCasing { get; set; } = false;

        public KeyValue(string key)
        {
            this.key = key;
            childrenIndex = new Dictionary<string, List<KeyValue>>();
            children = new List<KeyValue>();
        }

        public KeyValue(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public void addChild(KeyValue value)
        {
            if (!childrenIndex.ContainsKey(value.getKey()))
                childrenIndex.Add(value.getKey(), new List<KeyValue>());

            childrenIndex[value.getKey()].Add(value);
            children.Add(value);
        }

        public void addChild(string key, KeyValue value)
        {
            value.key = key;
            addChild(value);
        }

        public void addChild(string key, string value) { addChild(new KeyValue(key, value)); }

        public void clearChildren()
        {
            childrenIndex = new Dictionary<string, List<KeyValue>>();
            children = new List<KeyValue>();
        }

        public KeyValue findChildByKey(string key)
        {
            
            if (childrenIndex != null && childrenIndex.ContainsKey(key))
                return childrenIndex[key][0];

            if (childrenIndex != null)
                foreach (string indexKey in childrenIndex.Keys)
                    if (indexKey.ToLower() == key.ToLower())
                        return childrenIndex[indexKey][0];

            if (childrenIndex != null)
                foreach (string k in childrenIndex.Keys)
                    foreach (KeyValue child in childrenIndex[k])
                    {
                        KeyValue result = child.findChildByKey(key);
                        if (result != null)
                            return result;
                    }

            return null;
        }

        public List<KeyValue> findChildrenByKey(string key)
        {
            List<KeyValue> result = new List<KeyValue>();
            if (childrenIndex != null && childrenIndex.ContainsKey(key))
                result.AddRange(childrenIndex[key]);

            foreach (string indexKey in childrenIndex.Keys)
                if (indexKey.ToLower() == key.ToLower())
                    result.AddRange(childrenIndex[indexKey]);

            if (childrenIndex != null)
                foreach (string k in childrenIndex.Keys)
                    foreach (KeyValue child in childrenIndex[k])
                    {
                        result.AddRange(child.findChildrenByKey(key));
                    }

            return result;
        }

        public KeyValue getChildByKey(string key)
        {
            if (childrenIndex != null && childrenIndex.ContainsKey(key))
                return childrenIndex[key][0];

            foreach (string indexKey in childrenIndex.Keys)
            {
                if (indexKey.ToLower() == key.ToLower())
                {
                    return childrenIndex[indexKey][0];
                }
            }

            return null;
        }

        public List<KeyValue> getChildren() { return children; }

        public Dictionary<string, List<KeyValue>> getChildrenByKey() { return childrenIndex; }

        public List<KeyValue> getChildrenByKey(string key)
        {
            if (childrenIndex != null && childrenIndex.ContainsKey(key))
                return childrenIndex[key];

            foreach (string indexKey in childrenIndex.Keys)
                if (indexKey.ToLower() == key.ToLower())
                    return childrenIndex[indexKey];

            return null;
        }

        public string getKey() { return key; }

        public string getValue() { return value; }

        public string getValue(string key)
        {
            KeyValue child = getChildByKey(key);
            if (child != null)
                return child.getValue();

            return string.Empty;
        }

        public bool isParentKey() { return childrenIndex != null && value == null; }

        public void removeChild(KeyValue value)
        {
            if (children.Contains(value))
                children.Remove(value);

            if (childrenIndex.ContainsKey(value.key) && childrenIndex[value.key].Contains(value))
                childrenIndex[value.key].Remove(value);
        }

        public void setValue(string value)
        {
            if (this.value != null && childrenIndex == null)
                this.value = value;
        }

        public void setValue(string key, string value)
        {
            if (this.value != null || childrenIndex == null)
                return;

            KeyValue child = getChildByKey(key);
            if (child != null)
            {
                child.setValue(value);
                return;
            }

            addChild(key, new KeyValue(key, value));
        }

        public static string[] splitByWords(string fullString)
        {
            List<string> words = new List<string>();

            string[] parts = fullString.Split('\"');
            for (int i = 0; i < parts.Length; i++)
            {
                if (i % 2 == 1)
                {
                    // between quotes
                    string subpart = parts[i].Replace("\"", string.Empty);
                    words.Add(subpart);
                }
                else
                {
                    string[] subparts = parts[i].Split(null);
                    // outside quotes
                    foreach (string subpart in subparts)
                    {
                        if (subpart != string.Empty && subpart != " ")
                            words.Add(subpart);
                    }
                }
            }
            return words.ToArray();
        }

        public static void writeChunkFile(string path, KeyValue root) { 
            writeChunkFile(path, root, Encoding.UTF8); 
        }

        public static void writeChunkFile(string path, KeyValue root, bool quotes)
        { writeChunkFile(path, root, quotes, Encoding.UTF8); }

        public static void writeChunkFile(string path, KeyValue root, Encoding encoding)
        { writeChunkFile(path, root, true, encoding); }

        public static void writeChunkFile(string path, KeyValue root, bool quotes, Encoding encoding)
        {
            List<string> lines = writeChunkFileTraverse(root, 0, quotes);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, lines, encoding);
        }

        public static string writeChunk(KeyValue root, bool quotes)
        {
            List<string> lines = writeChunkFileTraverse(root, 0, quotes);
            return string.Join("\r\n", lines);
        }

        private static List<string> writeChunkFileTraverse(KeyValue node, int level, bool quotes)
        {
            List<string> lines = new List<string>();

            string tabs = string.Empty;
            for (int i = 0; i < level; i++)
            {
                tabs = tabs + "\t";
            }

            if (node.isParentKey()) // It's a parent key
            {
                if (node.key != string.Empty)
                {
                    lines.Add(tabs + (quotes ? "\"" : string.Empty) + node.key + (quotes ? "\"" : string.Empty));
                    lines.Add(tabs + "{");
                }

                foreach (KeyValue entry in node.getChildren())
                    lines.AddRange(writeChunkFileTraverse(entry, node.key != string.Empty ? level + 1 : level, quotes));

                if (node.key != string.Empty)
                    lines.Add(tabs + "}");
            }
            else if (node.getKey() != string.Empty && node.getValue() != null)   // It's a value key
            {
                string line = tabs;
                if (node.key != string.Empty)
                {
                    line = line +
                        (quotes ? "\"" : string.Empty) +
                        node.key +
                        (quotes ? "\"" : string.Empty) +
                        "\t\"" +
                        node.value +
                        "\"";
                }
                if (node.comment != string.Empty)
                {
                    if (node.key != string.Empty)
                        line = line + "\t";

                    line = line + "//" + node.comment;
                }

                lines.Add(line);
            }
            else if (node.comment != string.Empty)   // Comment line
            {
                string line = tabs + "//" + node.comment;
                lines.Add(line);
            }
            else if (node.key == string.Empty)     // Blank line
            {
                lines.Add("");
            }

            return lines;
        }

        public static KeyValue ReadChunk(string data)
        {
            return ReadChunk(data, false);
        }

        public static KeyValue ReadChunk(string data, bool hasOSInfo)
        {
            // Parse Valve chunkfile format
            List<KeyValue> list = new List<KeyValue>();

            ReadChunkData(data, hasOSInfo, list);
            return RefactorRoot(list);
        }

        public static KeyValue readChunkfile(string path)
        {
            return readChunkfile(path, false);
        }

        public static KeyValue readChunkfile(string path, bool hasOSInfo)
        {
            // Parse Valve chunkfile format
            List<KeyValue> list = new List<KeyValue>();

            readChunkfileStream(path, hasOSInfo, list);
            return RefactorRoot(list);
        }

        private static void ReadChunkData(string data, bool hasOSInfo, List<KeyValue> list)
        {
            Stack<KeyValuePair<string, KeyValue>> stack = new Stack<KeyValuePair<string, KeyValue>>();

            foreach (string line in Regex.Split(data, "\r\n|\r|\n"))
            {
                try
                {
                    readChunkLine(line, hasOSInfo, stack, list);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not read data. It's structure is broken.");
                }
            }
        }

        private static void readChunkfileStream(string path, bool hasOSInfo, List<KeyValue> list)
        {
            Stack<KeyValuePair<string, KeyValue>> stack = new Stack<KeyValuePair<string, KeyValue>>();
            if (File.Exists(path))
            {
                try
                {
                    using (StreamReader r = new StreamReader(path))
                    {
                        while (r.Peek() >= 0)
                        {
                            string line = r.ReadLine();
                            readChunkLine(line, hasOSInfo, stack, list);
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Could not read file \"" + path + "\". It's structure is broken.");
                }
            }
            else
            {
                MessageBox.Show("Could not find file \"" + path + "\" to read.");
            }
        }

        private static void readChunkLine(string line, bool hasOSInfo, Stack<KeyValuePair<string, KeyValue>> stack, List<KeyValue> list)
        {
            line = line.Trim();
            line = Regex.Replace(line, @"\s+", " ");

            // Remove comments of line
            if (line.Contains("//"))
                line = line.Substring(0, line.IndexOf("//"));

            string[] words = splitByWords(line);

            // If a line has a curly bracket inline (ex: gameinfo { ), let's split the lines, re-read them and return.
            if (line.Split('{').Length > 1 && !words[0].Contains("{"))
            {
                // Found a first case
                var lineSplit = line.Split('{');
                for(int i = 0; i < lineSplit.Length; i++)
                {
                    string substring = lineSplit[i];
                    if (i > 0)
                        substring = "{" + substring;

                    readChunkLine(substring, hasOSInfo, stack, list);
                }
            }
            else if(line.Split('}').Length > 1 && !words[0].Contains("}"))
            {
                var lineSplit = line.Split('}');
                for (int i = 0; i < lineSplit.Length; i++)
                {
                    string substring = lineSplit[i];
                    if (i > 0)
                        substring = "}" + substring;

                    readChunkLine(substring, hasOSInfo, stack, list);
                }
            }

            else if (line.StartsWith("//"))  // It's a standalone comment line
            {
                KeyValue comment = new KeyValue("");
                comment.comment = line.Substring(2);
                if (stack.Count > 0)
                    stack.Peek().Value.addChild(comment);
                else
                    list.Add(comment);
            }
            else if (words.Length == 0)    // It's a blank line
            {
                KeyValue blank = new KeyValue("");
                if (stack.Count > 0)
                    stack.Peek().Value.addChild(blank);
                else
                    list.Add(blank);
            }
            else if (words.Length > 0 && words[0].Contains("{")) // It opens a group
            {
                // We actually don't need to do anything.
            }
            else if (words.Length > 0 && words[0].Contains("}"))    // It closes a group
            {
                KeyValuePair<string, KeyValue> child = stack.Pop();
                if (stack.Count > 0)
                    stack.Peek().Value.addChild(child.Key, child.Value);
                else
                    list.Add(child.Value);
                //root = child.Value;
            }
            else if (words.Length == 1 || words.Length > 1 && words[1].StartsWith("//"))    // It's a parent key
            {
                line = line.Replace("\"", string.Empty);
                if (!KeyCasing)
                    line = line.ToLower();
                KeyValue parent = new KeyValue(line);
                stack.Push(new KeyValuePair<string, KeyValue>(line, new KeyValue(line)));
            }
            else if (words.Length >= 2)
            {
                // Workaround: Valve decided to use spaces in some .res files to identify the OS the key is used by.
                // So, this code will result in a false positive. Right now, the only example I have is of this kind:
                // HudHealth [$WIN32]
                // So, the workaround will be if the second word starts and ends with brackets
                if (hasOSInfo && words[1].StartsWith("[") && words[1].EndsWith("]")) // It's a parent key with a target OS
                {
                    line = line.Replace("\"", string.Empty);
                    KeyValue parent = new KeyValue(line);
                    stack.Push(new KeyValuePair<string, KeyValue>(line, new KeyValue(line)));
                }
                else // It's a value key
                {
                    string key = words[0].Replace("\"", string.Empty);
                    if (!KeyCasing)
                        key = key.ToLower();
                    KeyValue value = new KeyValue(key, words[1]);

                    if (stack.Count > 0)
                        stack.Peek().Value.addChild(key, value);
                    else
                        list.Add(value);
                }
            }
        }

        private static KeyValue RefactorRoot(List<KeyValue> list)
        {
            if (list.Count > 1)
            {
                KeyValue root = new KeyValue(string.Empty);
                foreach (KeyValue keyValue in list)
                    root.addChild(keyValue);

                return root;
            }
            else if (list.Count > 0)
            {
                //Debugger.Break();
                return list[0];
            }
                
            else
                return null;
        }
    }
}

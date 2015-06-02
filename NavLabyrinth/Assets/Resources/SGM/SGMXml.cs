using UnityEngine;
using System.Collections;
using System.Xml;

internal class SGMXml
{
	protected XmlDocument xmlDoc = null;

	private string _root = "root";

	protected virtual XmlNode GetRoot()
	{
		xmlDoc = LoadXMLFromAssest("save");
	
		return GetNodeByName(xmlDoc, _root);
	}

	protected XmlNode GetNodeByName(XmlNode a_parent, string a_name, string a_defaultValue = "")
	{
		XmlNode nodeFind = null;
		foreach(XmlNode node in a_parent.ChildNodes)
		{
			if(node.Name == a_name)
				nodeFind = node;
		}
		
		if(nodeFind == null)
		{
			nodeFind = xmlDoc.CreateElement(a_name);
			nodeFind.InnerXml = a_defaultValue;
			a_parent.AppendChild(nodeFind);
		}
		
		return nodeFind;
	}
	
	protected void SetNodeByName(XmlNode a_parent, string a_name, string a_value)
	{
		XmlNode node = GetNodeByName(a_parent, a_name);
		node.InnerText = a_value;
	}

	private XmlDocument LoadXMLFromAssest(string a_fileName)
	{
		// https://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading
		XmlDocument xmlDoc = new XmlDocument();
		if(System.IO.File.Exists(getPath()+a_fileName))
		{
			xmlDoc.LoadXml(System.IO.File.ReadAllText(getPath()+a_fileName));
		}
		else
		{
			string baseXml = 	"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n"+
								"<"+_root+">\n"+
								"</"+_root+">\n";
			xmlDoc.LoadXml(baseXml);
		}
		
		return xmlDoc;
	}
	
	// Following method is used to retrive the relative path as device platform
	protected string getPath(){
		#if UNITY_EDITOR
		return Application.dataPath +"/";
		#elif UNITY_ANDROID
		return Application.persistentDataPath;
		#elif UNITY_IPHONE
		return GetiPhoneDocumentsPath()+"/";
		#else
		return Application.dataPath +"/";
		#endif
	}

	private string GetiPhoneDocumentsPath()
	{
		// Strip "/Data" from path
		string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
		// Strip application name
		path = path.Substring(0, path.LastIndexOf('/'));
		return path + "/Documents";
	}
	
}


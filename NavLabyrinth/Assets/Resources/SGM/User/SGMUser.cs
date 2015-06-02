using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

internal class SGMUser : SGMXml
{
	internal string id = "";
	internal Dictionary<string, float> stats = new Dictionary<string, float>();

	private string _root = "Players";
	private string _subRoot = "Player";

	public SGMUser(string a_id)
	{
		id = a_id;

		SGMGameManager.Instance.userManager.RegisterUser(this);
	}

	protected override XmlNode GetRoot ()
	{
		XmlNode root = base.GetRoot ();

		return GetNodeByName(root, _root);
	}
	
	internal void Save()
	{
		// http://www.theappguruz.com/tutorial/unity-xml-parsing-unity/
		XmlNode root = GetRoot();

		XmlNode nodeParent = null;
		foreach(XmlNode node in root.SelectNodes(_subRoot))
		{
			if(node.HasChildNodes)
			{
				foreach(XmlNode childNode in node.ChildNodes)
				{
					if(childNode.Name == "id" && childNode.Value == id)
						nodeParent = node;
				}
			}
		}

		if(nodeParent == null)
		{
			nodeParent = GetNodeByName(root, _subRoot);
			SetNodeByName(nodeParent, "id", id);
		}

		_Save(nodeParent);

		xmlDoc.Save(getPath()+"/Resources/save.xml");
	}

	protected virtual void _Save(XmlNode a_node)
	{
		XmlNode statsNode = GetNodeByName(a_node, "stats");
		a_node.AppendChild(statsNode);
		foreach(string key in stats.Keys)
		{
			SetNodeByName(statsNode, key, stats[key].ToString());
		}
	}

	internal void Load()
	{
		XmlNode root = GetRoot();

		foreach(XmlNode node in root.SelectNodes(_subRoot))
		{
			if(node.HasChildNodes)
			{
				foreach(XmlNode childNode in node.ChildNodes)
				{
					if(childNode.Name == "id" && childNode.Value == id)
						_Load(childNode);
				}
			}
		}
	}
	
	protected virtual void _Load(XmlNode a_node)
	{
		XmlNode statsNode = GetNodeByName( a_node, "stats");
		foreach(XmlNode statNode in statsNode.ChildNodes)
		{
			stats[statNode.LocalName] = float.Parse(statsNode.InnerText);
		}
	}
}

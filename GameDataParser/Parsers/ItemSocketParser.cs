﻿using System.Collections.Generic;
using System.Xml;
using GameDataParser.Crypto.Common;
using GameDataParser.Files;
using Maple2Storage.Types.Metadata;

namespace GameDataParser.Parsers
{
    public class ItemSocketParser : Exporter<List<ItemSocketMetadata>>
    {
        public ItemSocketParser(MetadataResources resources) : base(resources, "item-socket") { }

        protected override List<ItemSocketMetadata> Parse()
        {
            List<ItemSocketMetadata> itemSockets = new List<ItemSocketMetadata>();
            foreach (PackFileEntry entry in Resources.XmlFiles)
            {
                if (!entry.Name.StartsWith("table/itemsocket"))
                {
                    continue;
                }

                // Parse XML
                XmlDocument document = Resources.XmlMemFile.GetDocument(entry.FileHeader);
                XmlNodeList properties = document.SelectNodes("/ms2/itemSocket");

                foreach (XmlNode property in properties)
                {
                    ItemSocketMetadata metadata = new ItemSocketMetadata();
                    metadata.Id = int.Parse(property.Attributes["id"].Value);
                    metadata.MaxCount = int.Parse(property.Attributes["maxCount"].Value);
                    metadata.FixedOpenCount = int.Parse(property.Attributes["fixOpenCount"].Value);

                    itemSockets.Add(metadata);
                }
            }
            return itemSockets;
        }
    }
}

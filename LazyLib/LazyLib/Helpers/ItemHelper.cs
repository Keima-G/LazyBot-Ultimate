namespace LazyLib.Helpers
{
    using LazyLib.Helpers.Vendor;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class ItemHelper
    {
        public static string GetNameById(uint entryId)
        {
            if (ItemDatabase.GetItem(entryId.ToString()) != null)
            {
                return ItemDatabase.GetItem(entryId.ToString())["item_name"].ToString();
            }
            Dictionary<string, string> wowHeadItem = WowHeadData.GetWowHeadItem((double) entryId);
            if (wowHeadItem != null)
            {
                string str = wowHeadItem["name"];
                string str2 = wowHeadItem["quality"];
                if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                {
                    ItemDatabase.PutItem(entryId.ToString(), str, str2);
                    return str;
                }
            }
            return "Unknown item";
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParsnipWebsite
{
    public class Enums
    {
        public enum Page
        {
            Home = 0
        }
    }

    public class PageIndex : IEquatable<PageIndex>, IEquatable<string>
    {
        public static readonly PageIndex Home = new PageIndex("home");
        public static readonly PageIndex Photos = new PageIndex("photos");
        public static readonly PageIndex Videos = new PageIndex("videos");
        public static readonly PageIndex Memes = new PageIndex("memes");
        public static readonly PageIndex Bios = new PageIndex("bios");
        public static readonly PageIndex MyUploads = new PageIndex("myuploads");
        public static readonly PageIndex Tag = new PageIndex("tag");
        public static readonly PageIndex Login = new PageIndex("login");
        public static readonly PageIndex Logout = new PageIndex("logout");
        public static readonly PageIndex Krakow = new PageIndex("krakow");
        public static readonly PageIndex Portugal = new PageIndex("portugal");
        public static readonly PageIndex Amsterdam = new PageIndex("amsterdam");
        public static readonly PageIndex EditMedia = new PageIndex("edit");
        public static readonly PageIndex View = new PageIndex("view");
        public static readonly PageIndex Search = new PageIndex("search");
        public static readonly PageIndex Latest = new PageIndex("latest");
        public static readonly PageIndex AdminManageMedia = new PageIndex("manage_media");

        private PageIndex(string value)
        {
            switch (value)
            {
                case "home":
                case "photos":
                case "videos":
                case "memes":
                case "bios":
                case "myuploads":
                case "tag":
                case "login":
                case "logout":
                case "krakow":
                case "portugal":
                case "amsterdam":
                case "edit":
                case "view":
                case "search":
                case "latest":
                case "manage_media":
                    Value = value;
                    break;
                default:
                    Value = "home";
                    break;

            }
        }

        public bool Equals(PageIndex other)
        {
            if (this.Value != other.Value) return false;

            return true;
        }

        public bool Equals(string other)
        {
            if (this.Value != other) return false;

            return true;
        }

        public override string ToString()
        {
            return Value;
        }

        public string Value { get; }

        
    }
}
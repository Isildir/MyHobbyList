using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobbyList.Models
{
    public enum ElementType
    {
        Book,
        Movie,
        Game
    }

    public enum AccountType
    {
        Pro,
        Normal
    }

    public enum AccountState
    {
        Active,
        Blocked
    }

    public enum FileType
    {
        Image,
        Binary
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MondaySoccer.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Player
    {
        public Player()
        {
            this.Game = new HashSet<Game>();
            this.Game_Player = new HashSet<Game_Player>();
        }
    
        public long PlayerID { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public long TeamID { get; set; }
    
        public virtual ICollection<Game> Game { get; set; }
        public virtual ICollection<Game_Player> Game_Player { get; set; }
        public virtual Team Team { get; set; }
    }
}

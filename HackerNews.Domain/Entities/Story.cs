using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Entities
{
    public class Story
    {
        
        public int id { get; set; }           
        public string title { get; set; }     
        public string url { get; set; }      
        public string author { get; set; }    
        public int score { get; set; }       
        public DateTime postedAt { get; set; }  
    }



}

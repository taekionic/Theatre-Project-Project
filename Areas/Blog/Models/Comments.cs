using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TheatreCMS3.Areas.Blog.Models
{
    public class Comments
    {
        [Key]
        public int CommentId { get; set; }
        public string Author { get; set; }
        public string Message { get; set; }
        public DateTime CommentDate { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }


        //add specific parameters when admin view enable
        public Comments()
        {
            CommentDate = DateTime.Now;
            Likes = 0;
            Dislikes = 0;
        }

        public string likesRatio()
        {
            double ratio = (Convert.ToDouble(Likes) / (Convert.ToDouble(Likes) + Convert.ToDouble(Dislikes))) * 100;
            string likeRatio = ratio.ToString("0");

            if (Likes == 0 && Dislikes == 0)
            {
                likeRatio = "50";
            }
            return likeRatio;
        }
    }
}
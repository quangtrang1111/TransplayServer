using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public class RutTrich
    {
        public static List<DAOs.Word> rutTrich()
        {


            HtmlWeb htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8  //Set UTF8 để hiển thị tiếng Việt
            };
            List<DAOs.Word> listTungvung = new List<DAOs.Word>();




            for (int i = 1; i < 100; i++)
            {
                string link = "http://600tuvungtoeic.com/index.php?mod=lesson&id=" + i.ToString();
                HtmlDocument document = htmlWeb.Load(link);

                if (document != null)
                {
                    var Div = document.DocumentNode.SelectSingleNode("//*[@id='tab4']");

                    if (Div != null)
                    {
                        var table = Div.SelectNodes("./div[(@class ='tuvung')]");

                        if (table != null)
                        {
                            for (int j = 1; j < table.Count; j++)
                            {
                                DAOs.Word tuvung = new DAOs.Word();

                                var hinhanh = table.ElementAt(j).SelectSingleNode(".//div[(@class = 'hinhanh')]");
                                var NoiDung = table.ElementAt(j).SelectSingleNode("./div[(@class='noidung')]");
                                var word = NoiDung.SelectNodes("./span");
                                tuvung.Name = word[0].InnerText;


                                string[] separatingChars = { "Giải thích:", "Từ loại:", "\n", "Ví dụ:", "\t" };
                                var layNghia = NoiDung.InnerText.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
                                tuvung.Means = layNghia[2];
                                string[] temp1 = { "src=", "\t" };
                                string[] temp2 = { "images", ">" };
                                string[] temp3 = { @"\" };
                                var index = hinhanh.InnerHtml.Split(temp1, System.StringSplitOptions.RemoveEmptyEntries);

                                tuvung.Picture = "http://600tuvungtoeic.com/images" + index[2].Split(temp2, System.StringSplitOptions.RemoveEmptyEntries)[1];
                                tuvung.Picture = tuvung.Picture.Split('"')[0];
                                listTungvung.Add(tuvung);
                            }
                        }
                    }
                }

            }

            return listTungvung;
        }
    }
}

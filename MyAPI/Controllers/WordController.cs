using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyAPI.Controllers
{
    [RoutePrefix("api/word")]
    public class WordController : ApiController
    {
        #region Helper
        public HttpResponseMessage CreateResponse<T>(HttpStatusCode statusCode, T data)
        {
            return Request.CreateResponse(statusCode, data);
        }

        public HttpResponseMessage CreateResponse(HttpStatusCode statusCode)
        {
            return Request.CreateResponse(statusCode);
        }
        #endregion

        [Authorize]
        [HttpGet]
        [Route("getallid")]
        public HttpResponseMessage GetAllID()
        {


          

            List<DAOs.Word> words = GLOBAL.db.Words.ToList();

            List<int> ids =new List<int>();

            for (int i = 0; i < words.Count; i++)
            {
                ids.Add(words[i].ID);
            }
            

            return CreateResponse(HttpStatusCode.OK, ids);
        }

        [Authorize]
        [HttpGet]
        [Route("getword")]
        public HttpResponseMessage GetWordByID(int id)
        {

            DAOs.Word word = GLOBAL.db.Words.Single(x => x.ID == id);
          
            if(word != null)
            { return CreateResponse(HttpStatusCode.OK, word); }
            return CreateResponse(HttpStatusCode.BadRequest);
        }

        [Authorize]
        [HttpGet]
        [Route("searchword")]
        public HttpResponseMessage GetWordByKeyword(string keyword)
        {

            List<DAOs.Word> words = GLOBAL.db.Words.Where(x => x.Name.Contains(keyword)).ToList();


            return CreateResponse(HttpStatusCode.OK, words);
        }

        [Authorize]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage AddWord(DAOs.Word word)
        {




            GLOBAL.db.Words.Add(word);
            GLOBAL.db.SaveChanges();
            return CreateResponse(HttpStatusCode.OK, word.ID);
        }



    }
}

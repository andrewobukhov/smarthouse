using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SmartHouseWeb.DataView;
using SmartHouseWeb.Models;
using System.Collections.Generic;

namespace SmartHouseWeb.Controllers
{
    public class SocketController : CommonApiController
    {
        public SocketStateDto GetState(int index)
        {
            var state = Context.SocketStates.FirstOrDefault(x => x.SocketIndex == (SocketIndex)index);

            if (state != null)
            {
                return new SocketStateDto { IsTurnOn = state.IsTurnOn };
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public IEnumerable<SocketStateDto> GetAllStates()
        {
            var list = new List<SocketStateDto>();
            foreach (var item in Context.SocketStates)
            {
                list.Add(new SocketStateDto(item));
            }
            return list;
        }

        [HttpPost]
        public SocketStateDto ChangeState([FromBody]SocketStateDto socketStateDto)
        {
            var state = Context.SocketStates.FirstOrDefault(x => x.SocketIndex == (SocketIndex)socketStateDto.Index);

            if (state != null)
            {
                state.IsTurnOn = !state.IsTurnOn;
                state.UpdateDate = DateTime.Now;
                Context.SaveChanges();

                return new SocketStateDto(state);
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
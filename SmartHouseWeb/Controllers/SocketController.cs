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
            var list = Context.SocketStates.Select(item => new SocketStateDto()
            {
                Name = "Розетка",
                IsTurnOn = item.IsTurnOn,
                Index = (int) item.SocketIndex
            });
            return list;
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage SetState(int index, bool isTurnOn)
        {
            var state = Context.SocketStates.FirstOrDefault(x => x.SocketIndex == (SocketIndex)index);

            if (state == null)
            {
                Context.SocketStates.Add(new SocketState { SocketIndex = (SocketIndex)index, IsTurnOn = isTurnOn });
            }
            else
            {
                state.IsTurnOn = isTurnOn;
            }

            Context.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
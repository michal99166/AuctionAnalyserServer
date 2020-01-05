using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.CQRS.Command;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAnalyserServer.Base.Controller
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected readonly ICqrsMediator DataBus;

        protected ApiControllerBase(ICqrsMediator dataBus)
        {
            this.DataBus = dataBus;
        }

    }
}
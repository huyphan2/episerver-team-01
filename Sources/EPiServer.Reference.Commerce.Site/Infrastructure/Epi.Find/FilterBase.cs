using EPiServer.Find;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPiServer.Reference.Commerce.Site.Infrastructure.Epi.Find
{
    public class FilterBase
    {
        protected readonly IClient FindClient = EpiserverFind.Instance.Create();
    }
}

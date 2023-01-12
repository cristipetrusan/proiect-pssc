using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.CartModel
{
    public record PayCartCommand
    {
        public PayCartCommand(IReadOnlyCollection<UnvalidatedItem> inputCartItems)
        {
            InputCartItems = inputCartItems;
        }

        public IReadOnlyCollection<UnvalidatedItem> InputCartItems { get; }
    }
}

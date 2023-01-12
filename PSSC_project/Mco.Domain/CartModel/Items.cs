using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessing.Domain.CartModel
{
    [AsChoice]
    public static partial class Items
    {
        public interface IItems { }

        public record UnvalidatedItems: IItems
        {
            public UnvalidatedItems(IReadOnlyCollection<UnvalidatedItem> itemList)
            {
                ItemList = itemList;
            }

            public IReadOnlyCollection<UnvalidatedItem> ItemList { get; }
        }

        public record InvalidItems: IItems
        {
            internal InvalidItems(IReadOnlyCollection<UnvalidatedItem> itemList, string reason)
            {
                ItemList = itemList;
                Reason = reason;
            }

            public IReadOnlyCollection<UnvalidatedItem> ItemList { get; }
            public string Reason { get; }
        }

        public record ValidatedItems: IItems
        {
            internal ValidatedItems(IReadOnlyCollection<ValidatedItem> itemList)
            {
                ItemList = itemList;
            }

            public IReadOnlyCollection<ValidatedItem> ItemList { get; }
        }

        // public record CalculatedExamGrades : IItems
        // {
        //     internal CalculatedExamGrades(IReadOnlyCollection<CalculatedSudentGrade> gradesList)
        //     {
        //         ItemList = gradesList;
        //     }

        //     public IReadOnlyCollection<CalculatedSudentGrade> ItemList { get; }
        // }

        public record PaidItems : IItems
        {
            internal PaidItems(IReadOnlyCollection<ValidatedItem> itemList, string price, DateTime publishedDate)
            {
                ItemList = itemList;
                PublishedDate = publishedDate;
                Price = price;
            }

            public IReadOnlyCollection<ValidatedItem> ItemList { get; }
            public DateTime PublishedDate { get; }
            public string Price { get; }
        }
    }
}

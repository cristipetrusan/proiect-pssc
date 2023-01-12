// using LanguageExt;
// using System;
// using static LanguageExt.Prelude;

// namespace OrderProcessing.Domain.CartModel
// {
//     public record Item
//     {
//         public int Id { get; }
//         public decimal Amount { get; }

//         public Item(int id, int amount)
//         {
//             if (IsValid(amount))
//             {
//                 Id = id;
//                 Amount = amount;
//             }
//             // else
//             // {
//             //     throw new InvalidGradeException($"{value:0.##} is an invalid grade value.");
//             // }
//         }

//         // public Item Round()
//         // {
//         //     var roundedValue = Math.Round(Price);
//         //     return new Item(roundedValue);
//         // }

//         // public override string ToString()
//         // {
//         //     return $"{Price:0.##}";
//         // }

//         public static Option<Item> TryParseItem(string itemIdString, string amount)
//         {
//             if(Int32.TryParse(itemIdString, out int id) && int.TryParse(amount))
//             {
//                 return Some<Item>(new(numericPrice));
//             }
//             else
//             {
//                 return None;
//             }
//         }

//         private static bool IsValid(int amount) => amount > 0;
//     }
// }

using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using UiValidation.Extensions;
using UiValidation.Models;

namespace UiValidation.Controllers
{
    public class InboxController : Controller
    {
        public IActionResult Index(string search)
        {
            using (var context = new InboxContext())
            {
                var query = context.Inboxes.AsQueryable();

                if (search != null && search.Length > 0)
                {
                    query = query.Where(Filter(search));
                }

                var final = query.ToList();

                ViewBag.Search = search ?? "";

                return View(final);
            }
        }

        private Expression<Func<Inbox, bool>> Filter(string search)
        {
            Expression<Func<Inbox, bool>> predicate = m => m.Sender.Contains(search) || m.Recipient.Contains(search) || m.Subject.Contains(search) || m.Body.Contains(search);

            var matchers = ParseNumbers(search);
            if (matchers?.Count > 0)
            {
                matchers.ForEach(matcher =>
                {
                    Expression<Func<Inbox, bool>> datePredicate = null;

                    if (matcher.Day.HasValue)
                        datePredicate = AppendPredicate(matcher.UsePartialMatch, datePredicate, m => m.Timestamp.Value.Day == matcher.Day);
                    if (matcher.Month.HasValue)
                        datePredicate = AppendPredicate(matcher.UsePartialMatch, datePredicate, m => m.Timestamp.Value.Month == matcher.Month);
                    if (matcher.Year.HasValue)
                        datePredicate = AppendPredicate(matcher.UsePartialMatch, datePredicate, m => m.Timestamp.Value.Year == matcher.Year);

                    if (datePredicate == null) return;

                    predicate = predicate.Or(AppendPredicate(false, m => m.Timestamp.HasValue, datePredicate));
                });
            }

            return predicate;
        }

        private Expression<Func<Inbox, bool>> AppendPredicate(bool useOr, Expression<Func<Inbox, bool>> expression1, Expression<Func<Inbox, bool>> expression2)
        {
            if (expression1 == null) return expression2;
            if (expression2 == null) return expression1;
            
            if (useOr)
            {
                return expression1.Or(expression2);
            }
            else
            {
                return expression1.And(expression2);
            }
        }

        private List<DateTimeMatcher> ParseNumbers(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return new List<DateTimeMatcher>();
            }
            var numbers = search.Split(' ')[0]
                .Split('/', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(new List<Int16?>(), (res, next) =>
                {
                    if (Int16.TryParse(next, out var number))
                    {
                        res.Add(number);
                    } else
                    {
                        res.Add(null);
                    }
                    return res;
                });

            switch (numbers.Count)
            {
                case 0:
                    return new List<DateTimeMatcher>();
                case 1:
                    return new List<DateTimeMatcher>
                    {
                        new DateTimeMatcher{
                            Day = numbers[0],
                            Month = numbers[0],
                            Year = numbers[0],
                            UsePartialMatch = true
                        }
                    };
                case 2:
                    return new List<DateTimeMatcher>
                    {
                        new DateTimeMatcher{
                            Month = numbers[0],
                            Day = numbers[1]
                        },
                        new DateTimeMatcher{
                            Day = numbers[0],
                            Year = numbers[1]
                        }
                    };
                default:
                    return new List<DateTimeMatcher>
                    {
                        new DateTimeMatcher{
                            Month = numbers[0],
                            Day = numbers[1],
                            Year = numbers[2]
                        }
                    };
            }
        }
    }
}

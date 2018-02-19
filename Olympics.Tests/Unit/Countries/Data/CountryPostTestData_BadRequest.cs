using Olympics.Domain.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Olympics.Tests.Unit.Countries.Data
{
    public class CountryPostTestData_BadRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { null as Country }
        };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

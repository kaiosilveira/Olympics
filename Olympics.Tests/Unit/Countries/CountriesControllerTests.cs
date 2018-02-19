using Moq;
using Olympics.Domain.Contracts.Interfaces;
using Olympics.Domain.Entities;
using Olympics.Tests.Unit.Countries.Data;
using Olympics.Web.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using Xunit;

namespace Olympics.Tests.Unit.Countries
{
    public class CountriesControllerTests
    {
        [Fact]
        public void List_ShouldReturnCorrectCountries()
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();
            var countriesTemplateList = new List<Country>()
            {
                new Country()
                {
                    Id = 1,
                    Name = "Brasil",
                    GoldMedals = 32,
                    SilverMedals = 434,
                    BronzeMedals = 43
                },
                new Country()
                {
                    Id = 2,
                    Name = "EUA",
                    GoldMedals = 23,
                    SilverMedals = 432,
                    BronzeMedals = 1231
                }
            };

            mockedCountriesRepository.Setup(rep => rep.List())
                .Returns(countriesTemplateList)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var response = controller.Get() as OkNegotiatedContentResult<IEnumerable<Country>>;

            Assert.NotNull(response);
            Assert.NotNull(response.Content);
            Assert.NotEmpty(response.Content);
            
            for(var i = 0; i < response.Content.Count(); i++)
            {
                Assert.Equal(response.Content.ElementAt(i).Id, countriesTemplateList.ElementAt(i).Id);
                Assert.Equal(response.Content.ElementAt(i).Name, countriesTemplateList.ElementAt(i).Name);
                Assert.Equal(response.Content.ElementAt(i).GoldMedals, countriesTemplateList.ElementAt(i).GoldMedals);
                Assert.Equal(response.Content.ElementAt(i).SilverMedals, countriesTemplateList.ElementAt(i).SilverMedals);
                Assert.Equal(response.Content.ElementAt(i).BronzeMedals, countriesTemplateList.ElementAt(i).BronzeMedals);
            }
            
            mockedCountriesRepository.VerifyAll();
        }

        [Theory]
        [InlineData(1)]
        public void Get_ShouldReturnCorrectResponse(int id)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();
            var countryTemplate = new Country() { Id = 1, Name = "Brasil", GoldMedals = 1, SilverMedals = 1, BronzeMedals = 1 };
            
            mockedCountriesRepository.Setup(rep => rep.Get(It.IsAny<int>()))
                .Returns(countryTemplate)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Get(id);
            var response = httpResult as OkNegotiatedContentResult<Country>;

            Assert.IsType<OkNegotiatedContentResult<Country>>(httpResult);
            
            Assert.Equal(countryTemplate.Id, response.Content.Id);
            Assert.Equal(countryTemplate.Name, response.Content.Name);
            Assert.Equal(countryTemplate.GoldMedals, response.Content.GoldMedals);
            Assert.Equal(countryTemplate.SilverMedals, response.Content.SilverMedals);
            Assert.Equal(countryTemplate.BronzeMedals, response.Content.BronzeMedals);
            
            mockedCountriesRepository.VerifyAll();
        }

        [Theory]
        [InlineData(4)]
        public void Get_ShouldReturnNotFound(int id)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();

            mockedCountriesRepository.Setup(r => r.Get(id))
                .Returns(null as Country)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Get(id);
            var response = httpResult as OkNegotiatedContentResult<Country>;

            Assert.IsType<NotFoundResult>(httpResult);
            Assert.Null(response);

            mockedCountriesRepository.VerifyAll();
        }

        [Theory]
        [InlineData(0)]
        public void Get_ShouldReturnBadRequestForInvalidParameters(int id)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();
            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Get(id);
            var response = httpResult as OkNegotiatedContentResult<Country>;
            
            Assert.IsType<BadRequestErrorMessageResult>(httpResult);
            Assert.Null(response);
        }

        [Theory]
        [ClassData(typeof(CountryPutTestData_Ok))]
        public void Put_ShouldReturnCorrectResponse(int id, Country country)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();
           
            mockedCountriesRepository.Setup(rep => rep.Update(It.IsAny<Country>()))
                .Returns(1)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Put(id, country);

            Assert.NotNull(httpResult);
            Assert.IsType<OkNegotiatedContentResult<int>>(httpResult);

            var response = httpResult as OkNegotiatedContentResult<int>;

            Assert.IsType<int>(response.Content);
            Assert.Equal(1, response.Content);
        }

        [Theory]
        [ClassData(typeof(CountryPutTestData_BadRequest))]
        public void Put_ShouldReturnBadRequestForMalformedRequests(int id, Country country)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();

            mockedCountriesRepository.Setup(rep => rep.Update(It.IsAny<Country>()))
                .Returns(1)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Put(id, country);
            
            Assert.IsType<BadRequestErrorMessageResult>(httpResult);
        }

        [Theory]
        [ClassData(typeof(CountryPostTestData_Ok))]
        public void Post_ShouldReturnCorrectResponse(Country country)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();

            mockedCountriesRepository.Setup(rep => rep.Create(It.IsAny<Country>()))
                .Returns(1)
                .Verifiable();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var response = controller.Post(country) as OkNegotiatedContentResult<int>;

            Assert.IsType<int>(response.Content);
            Assert.Equal(1, response.Content);
        }

        [Theory]
        [ClassData(typeof(CountryPostTestData_BadRequest))]
        public void Post_ShouldReturnBadRequestForMalformedRequests(Country country)
        {
            var mockedCountriesRepository = new Mock<ICountriesRepository>();

            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Post(country);

            Assert.IsType<BadRequestErrorMessageResult>(httpResult);
        }

        [Theory]
        [InlineData(1)]
        public void Delete_ShouldReturnCorrectResponse(int id)
        {

            var mockedCountriesRepository = new Mock<ICountriesRepository>();

            mockedCountriesRepository.Setup(r => r.Delete(It.IsAny<int>()))
                .Returns(1)
                .Verifiable();
            
            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Delete(id);

            Assert.IsType<OkNegotiatedContentResult<int>>(httpResult);
            Assert.Equal(1, (httpResult as OkNegotiatedContentResult<int>).Content);
            
            mockedCountriesRepository.VerifyAll();
        }

        [Theory]
        [InlineData(0)]
        public void Delete_ShouldReturnBadRequestForMalformedRequests(int id)
        {

            var mockedCountriesRepository = new Mock<ICountriesRepository>();
            
            var controller = new CountriesController(mockedCountriesRepository.Object);
            var httpResult = controller.Delete(id);

            Assert.IsType<BadRequestErrorMessageResult>(httpResult);
            
            mockedCountriesRepository.VerifyAll();
        }
    }
}

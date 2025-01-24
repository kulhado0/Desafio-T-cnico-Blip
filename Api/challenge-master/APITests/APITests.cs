using blip_teste_api.Controllers;
using blip_teste_api.Exceptions;
using blip_teste_api.Models;
using Microsoft.Extensions.Logging;

namespace APITests
{
    public class APITests
    {
        private readonly HttpClient _httpClient;
        private readonly DesafiosController desafiosController;

        public APITests()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            var logger = loggerFactory.CreateLogger<DesafiosController>();
            desafiosController = new DesafiosController(logger);
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "blip_teste_api");
        }

        // TODO: Estes testes deveriam estar em arquivos diferentes (mais files de testes específicas para cada caso - prazo de entrega)
        // TODO: Melhorar/Adicionar testes
        [Fact]
        public async void ShouldGetRepositoriesWhenCallGitHubAPI()
        {
            var response = await desafiosController.GetTakeRepositories();
            Assert.NotEmpty(response);
        }


        [Fact]
        public async void ShouldCreateGithubRepositoriesListWhenCallGitHubAPI()
        {
            var response = await desafiosController.GetTakeRepositories();
            List<GithubRepository>? repositories = desafiosController.CreateGithubRepositoriesList(response);
            Assert.NotNull(repositories);
        }

        [Fact]
        public void ShouldRaiseNoRepositoriesFoundExceptionWhenEmptyGitHubRepositoriesList()
        {
            List<GithubRepository>? repositories = null;
            Assert.Throws<NoRepositoriesFoundException>(() => desafiosController.ValidateGitHubRepositories(repositories));
        }

        [Fact]
        public async void ShouldSerializeCarouselItemsListWhenCallGitHubAPI()
        {
            string response = await desafiosController.GetTakeRepositories();
            List<GithubRepository>? repositories = desafiosController.CreateGithubRepositoriesList(response);
            List<CarouselItem>? carouselItems = desafiosController.SerializeCarouselItems(repositories);
            Assert.NotNull(carouselItems);
        }

        [Fact]
        public void ShouldRaiseNoCarouselItemsFoundExceptionWhenEmptyCarouselItems()
        {
            List<CarouselItem>? carouselItems = null;
            Assert.Throws<NoCarouselItemsFoundException>(() => desafiosController.ValidateCarouselItems(carouselItems));
        }

        [Fact]
        public async void ShouldSerializeAPIResponseWhenValidFlow()
        {
            string response = await desafiosController.GetTakeRepositories();
            List<GithubRepository>? repositories = desafiosController.CreateGithubRepositoriesList(response);
            List<CarouselItem>? carouselItems = desafiosController.SerializeCarouselItems(repositories);
            ResponseObject responseObject = desafiosController.SerializeAPIResponse(carouselItems);
            Assert.NotNull(responseObject);
        }

        [Fact]
        public async void ShouldntRaiseNoCarouselItemsFoundExceptionWhenValidCarouselItems()
        {
            string response = await desafiosController.GetTakeRepositories();
            List<GithubRepository>? repositories = desafiosController.CreateGithubRepositoriesList(response);
            List<CarouselItem>? carouselItems = desafiosController.SerializeCarouselItems(repositories);
            desafiosController.ValidateCarouselItems(carouselItems);
        }

        [Fact]
        public async void ShouldntRaiseNoRepositoriesFoundExceptionWhenValidGitHubRepositoriesList()
        {
            string response = await desafiosController.GetTakeRepositories();
            List<GithubRepository>? repositories = desafiosController.CreateGithubRepositoriesList(response);
            desafiosController.ValidateGitHubRepositories(repositories);
        }

        [Fact]
        public void ShouldGetDefaultBlipAvatarUrlWhenCallMethod()
        {
            string response = desafiosController.GetDefaultBlipAvatarUrl();
            Assert.NotEmpty(response);
        }
    }
}
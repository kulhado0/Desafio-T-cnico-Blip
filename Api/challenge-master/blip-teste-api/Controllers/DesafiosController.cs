using blip_teste_api.Exceptions;
using blip_teste_api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Label = blip_teste_api.Models.Label;

namespace blip_teste_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DesafiosController : ControllerBase
    {
        private readonly ILogger<DesafiosController> _logger;
        private readonly HttpClient _httpClient;

        public DesafiosController(ILogger<DesafiosController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "blip_teste_api");
        }

        [HttpGet(Name = "GetDesafios")]
        [ProducesResponseType(typeof(ResponseObject), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 500)]
        public async Task<IActionResult> Get()
        {
            try
            {
                string response = await GetTakeRepositories();

                List<GithubRepository>? repositories = CreateGithubRepositoriesList(response);
                ValidateGitHubRepositories(repositories);

                List<CarouselItem>? carouselItems = SerializeCarouselItems(repositories);
                ValidateCarouselItems(carouselItems);

                ResponseObject responseObject = SerializeAPIResponse(carouselItems);
                return Ok(responseObject);
            }
            // TODO: Implementar resposta de erro no BOT ao obter resposta da API -> igual ao proposto no figma (cuidado prazo de entrega)
            catch (NoRepositoriesFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (NoCarouselItemsFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao buscar repositórios do GitHub");
                return StatusCode(500, "Erro ao buscar dados do GitHub");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocorreu um erro inesperado");
                return StatusCode(500, "Ocorreu um erro inesperado");
            }
        }

        // TODO: Nada a partir desta linha deveria estar aqui (cuidado prazo de entrega)
        // TODO: Isolar esses métodos em novos arquivos (estou deixando public por causa dos testes - fazer o fix)
        [ApiExplorerSettings(IgnoreApi = true)]
        public ResponseObject SerializeAPIResponse(List<CarouselItem>? carouselItems)
        {
            return new ResponseObject
            {
                itemType = "application/vnd.lime.document-select+json",
                items = carouselItems
            };
        }

        // TODO: Chain Of Responsability -> (Over Engineering)
        [ApiExplorerSettings(IgnoreApi = true)]
        public void ValidateCarouselItems(List<CarouselItem>? carouselItems)
        {
            if (carouselItems == null || !carouselItems.Any())
            {
                throw new NoCarouselItemsFoundException("Não foi possível carregar os itens do carrossel.");
            }
        }

        // TODO: Quebrar mais a função
        [ApiExplorerSettings(IgnoreApi = true)]
        public List<CarouselItem>? SerializeCarouselItems(List<GithubRepository>? repositories)
        {
            string blipAvatarUrl = GetDefaultBlipAvatarUrl();
            return repositories?.Select(repo => new CarouselItem
            {
                header = new Header
                {
                    type = "application/vnd.lime.media-link+json",
                    value = new HeaderValue
                    {
                        title = repo.FullName,
                        text = repo.Description ?? "Sem descrição",
                        type = "image/jpeg",
                        uri = blipAvatarUrl
                    }
                },
                // Implementei o botão pois não achei prático o carrossel sem algum redirecionamento (sei que não foi proposto)
                options = new object[]
                {
                    new Option
                    {
                        label = new Label
                        {
                            type = "application/vnd.lime.web-link+json",
                            value = new WebLinkValue
                            {
                                title = "Ver Repositório",
                                uri = repo.HtmlUrl
                            }
                        }
                    }
                }
            }).ToList();
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetDefaultBlipAvatarUrl()
        {
            return "https://avatars.githubusercontent.com/u/4369522?v=4";
        }

        // TODO: Chain Of Responsability -> (Over Engineering)
        [ApiExplorerSettings(IgnoreApi = true)]
        public void ValidateGitHubRepositories(List<GithubRepository>? repositories)
        {
            if (repositories == null || repositories.Count == 0)
            {
                throw new NoRepositoriesFoundException("Nenhum repositório encontrado para a organização.");
            }
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public List<GithubRepository>? CreateGithubRepositoriesList(string response)
        {
            return JsonSerializer.Deserialize<List<GithubRepository>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        // TODO: Pegar apenas os repositórios c# (cuidado prazo de entrega) -> Atualmente está pegando os rep. de qualquer linguagem
        // x.AddRange(repositories.Where(repo => repo.Language?.Equals("C#", StringComparison.OrdinalIgnoreCase) == true));
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetTakeRepositories(int perPage = 5, string sort = "created", int page = 1)
        {
            string url = $"https://api.github.com/orgs/takenet/repos?sort={sort}&direction=asc&per_page={perPage}&page={page}";
            var response = await _httpClient.GetStringAsync(url);
            return response;
        }
    }
}

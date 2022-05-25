using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SENAI_UC14_E3.Interfaces;
using SENAI_UC14_E3.Models;

namespace SENAI_UC14_E3.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _iUsuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _iUsuarioRepository = usuarioRepository;
        }
        /// <summary>
        /// Este método lista todos os dados da entidade Usuarios do banco.
        /// </summary>
        /// <returns>Usuarios listados.</returns>
        /// <response code="500">O servidor encontrou uma situação com a qual não sabe lidar.</response>
        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_iUsuarioRepository.Listar());
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// Este método retorna o usuário do o id selecionado.
        /// </summary>
        /// <param name="id">Passar o id do usuario.</param>
        /// <returns>Usuário encontrado.</returns>
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                {
                    return NotFound();
                }

                return Ok(usuarioEncontrado);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Este método cadastra um novo usuário.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "email": "string",
        ///        "senha": "string",
        ///        "tipo": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="usuario"></param>
        /// <returns>Cria um novo usuário.</returns>
        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            try
            {
                _iUsuarioRepository.Cadastrar(usuario);

                return StatusCode(201);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Este método altera um usuário existente através do id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "email": "string",
        ///        "senha": "string",
        ///        "tipo": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="usuario"></param>
        /// <returns>Informações do usuário alteradas.</returns>
        [HttpPut("{id}")]
        public IActionResult Alterar(int id, Usuario usuario)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                {
                    return NotFound();
                }

                _iUsuarioRepository.Atualizar(id, usuario);

                return Ok("Usuario Alterado");
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Este método exlui um usuário através do id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Usuario deletado.</returns>
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                if (usuarioEncontrado == null)
                {
                    return NotFound();
                }

                _iUsuarioRepository.Deletar(id);

                return Ok("Usuario deletado");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using QuickStartWebApi2.DTOs;
using QuickStartWebApi2.Interfaces;
using QuickStartWebApi2.Models;
using System;

namespace QuickStartWebApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<IActionResult> GetPessoas()
        {
            try
            {
                var pessoas = await _pessoaRepository.GetAllAsync();
                var pessoasDto = pessoas.Select(p => new PessoaDto
                {
                    Nome = p.Nome,
                    DataNascimento = p.DataNascimento,
                    Endereco = p.Endereco
                });

                return Ok(pessoasDto);
            } catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Um erro ocorreu ao obter as pessoas: " + ex.Message);
            }
        }

        // GET: api/Pessoa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPessoa(int id)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetByIdAsync(id);

                if (pessoa == null)
                {
                    return NotFound("Pessoa não encontrada pelo id informado.");
                }

                var pessoaDto = new PessoaDto
                {
                    Nome = pessoa.Nome,
                    DataNascimento = pessoa.DataNascimento,
                    Endereco = pessoa.Endereco
                };

                return Ok(pessoaDto);
            } catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Um erro ocorreu ao obter a pessoa: " + ex.Message);
            }
        }

        /// <summary>
        /// Recupera uma lista de pessoas pesquisando pelo nome.
        /// </summary>
        /// <returns>Retorna a lista de pessoas pesquisando pelo nome</returns>
        /// <response code="200">Se a lista foi obtida com sucesso</response>

        // GET: api/Pessoa/search/{name}
        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<PessoaDto>>> SearchByName(string name)
        {
            try
            {

                var pessoas = await _pessoaRepository.GetByNameAsync(name);
                var pessoasDto = pessoas.Select(p => new PessoaDto
                {
                    Nome = p.Nome,
                    DataNascimento = p.DataNascimento,
                    Endereco = p.Endereco
                });

                return Ok(pessoasDto);


            } catch (Exception ex)
            {
                return StatusCode(500, "Um erro ocorreu ao pesquisar uma pessoa pelo nome: " + ex.Message);
            }
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<IActionResult> CreatePessoa([FromBody] PessoaDto pessoaDto)
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = pessoaDto.Nome,
                    DataNascimento = pessoaDto.DataNascimento,
                    Endereco = pessoaDto.Endereco
                };

                await _pessoaRepository.AddAsync(pessoa);

                return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.PessoaId }, pessoa);
            } catch (Exception ex)
            {

                return StatusCode(500, "Um erro ocorreu ao criar a pessoa: " + ex.Message);
            }
        }

        // PUT: api/Pessoa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePessoa(int id, [FromBody] PessoaDto pessoaDto)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetByIdAsync(id);
                if (pessoa == null)
                {
                    return NotFound("Pessoa não encontrada pelo id informado.");
                }

                pessoa.Nome = pessoaDto.Nome;
                pessoa.DataNascimento = pessoaDto.DataNascimento;
                pessoa.Endereco = pessoaDto.Endereco;

                await _pessoaRepository.UpdateAsync(pessoa);

                return NoContent();
            } catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Um erro ocorreu ao atualizar a pessoa: " + ex.Message);
            }
        }

        // DELETE: api/Pessoa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetByIdAsync(id);
                if (pessoa == null)
                {
                    return NotFound("Pessoa não encontrada pelo id informado.");
                }

                await _pessoaRepository.DeleteAsync(pessoa);

                return NoContent();
            } catch (Exception ex)
            {
                // Log the exception details
                return StatusCode(500, "Um erro ocorreu ao excluir a pessoa: " + ex.Message);
            }
        }


    }
}

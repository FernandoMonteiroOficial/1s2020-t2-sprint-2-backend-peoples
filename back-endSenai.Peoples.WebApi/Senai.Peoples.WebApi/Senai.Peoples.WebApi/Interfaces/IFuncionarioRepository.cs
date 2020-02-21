using Senai.Peoples.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Peoples.WebApi.Interfaces
{
    interface IFuncionarioRepository
    {
        List<FuncionarioDomain> Listar();

        string Cadastrar(FuncionarioDomain funcionario);

        string AtualizarIdUrl(int id, FuncionarioDomain genero);

        FuncionarioDomain BuscarPorId(int id);

        string Deletar(int id);

    }
}
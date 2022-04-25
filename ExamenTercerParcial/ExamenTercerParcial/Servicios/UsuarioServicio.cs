using Datos.Interfaces;
using Datos.Repositorios;
using ExamenTercerParcial.Data;
using ExamenTercerParcial.Interfaces;
using Modelos;

namespace ExamenTercerParcial.Servicios;

public class UsuarioServicio : IUsuarioServicio
{

    private readonly MySQLConfiguration _configuration;
    private IUsuarioRepositorio usuarioRepositorio;

    public UsuarioServicio(MySQLConfiguration configuration)
    {
        _configuration = configuration;
        usuarioRepositorio = new UsuarioRepositorio(configuration.CadenaConexion);
    }

    public async Task<bool> Actualizar(Usuario usuario)
    {
        return await usuarioRepositorio.Actualizar(usuario);
    }

    public async Task<bool> Eliminar(Usuario usuario)
    {
       return await usuarioRepositorio.Eliminar(usuario);
    }

    public async Task<IEnumerable<Usuario>> GetLista()
    {
        return await usuarioRepositorio.GetLista();
    }

    public Task<Usuario> GetPorCodigo(string codigo)
    {
        return usuarioRepositorio.GetPorCodigo(codigo);
    }

    public async Task<bool> Nuevo(Usuario usuario)
    {
        return await usuarioRepositorio.Nuevo(usuario);
    }
}


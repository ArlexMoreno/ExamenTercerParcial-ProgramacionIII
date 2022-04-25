using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;

namespace Datos.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{

    private string CadenaConexion;
    public UsuarioRepositorio(string cadenaConexion)
    {
        CadenaConexion = cadenaConexion;
    }

    private MySqlConnection Conexion()
    {
        return new MySqlConnection(CadenaConexion);
    }

    public async Task<bool> Actualizar(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "UPDATE usuario SET Codigo = @Codigo, Nombre = @Nombre, Rol = @Rol, Clave = @Clave, EstaActivo = @EstaActivo WHERE Codigo = @Codigo ;";
            resultado = await conexion.ExecuteAsync(sql, new
            {
                usuario.Codigo,
                usuario.Nombre,
                usuario.Rol,
                usuario.Clave,
                usuario.EstaActivo
            });
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> Eliminar(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "DELETE FROM usuario WHERE Codigo = @Codigo;";
            resultado = await conexion.ExecuteAsync(sql, new { usuario.Codigo });
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<IEnumerable<Usuario>> GetLista()
    {
        IEnumerable<Usuario> lista = new List<Usuario>();

        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT * FROM usuario;";
            lista = await conexion.QueryAsync<Usuario>(sql);
        }
        catch (Exception ex)
        {
        }
        return lista;
    }


    public async Task<Usuario> GetPorCodigo(string codigo)
    {
        Usuario user = new Usuario();
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT * FROM usuario WHERE Codigo = @Codigo;";
            user = await conexion.QueryFirstAsync<Usuario>(sql, new { codigo });
        }
        catch (Exception)
        {
        }
        return user;
    }

    public async Task<bool> Nuevo(Usuario usuario)
    {
        int resultado;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "INSERT INTO usuario (Codigo, Nombre, Rol, Clave, EstaActivo) VALUES (@Codigo, @Nombre, @Rol, @Clave, @EstaActivo)";
            resultado = await conexion.ExecuteAsync(sql, usuario);
            return resultado > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> ValidaUsuario(Login login)
    {
        bool valido = false;
        try
        {
            using MySqlConnection conexion = Conexion();
            await conexion.OpenAsync();
            string sql = "SELECT 1 FROM usuario WHERE Codigo = @Codigo AND Clave = @Clave;";
            valido = await conexion.ExecuteScalarAsync<bool>(sql, new { login.Codigo, login.Clave });
        }
        catch (Exception ex)
        {
        }
        return valido;
    }
    
}


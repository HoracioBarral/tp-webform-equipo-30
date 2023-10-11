using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public object MessageBox { get; private set; }

        public List<Articulo> listar()
        {
            List<Articulo> listaArt = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select a.id,a.Codigo,a.Nombre,a.Descripcion,a.Precio,c.Descripcion Tipo,m.Descripcion Marca,m.Id idMarca, c.Id idCategoria from articulos a left join categorias c on a.IdCategoria=c.Id inner join marcas m on a.IdMarca=m.Id");
            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)datos.Lector["id"];
                    if(!(datos.Lector["Codigo"] is DBNull))
                        art.Codigo = (string)datos.Lector["Codigo"];
                    if (!(datos.Lector["Nombre"] is DBNull))
                        art.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        art.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["Precio"] is DBNull))
                        art.Precio = (decimal)datos.Lector["Precio"];
                    if (!(datos.Lector["Tipo"] is DBNull))
                    {
                        art.TipoCategoria = new Categoria();
                        art.TipoCategoria.Descripcion = (string)datos.Lector["Tipo"];
                        if (!(datos.Lector["idCategoria"] is DBNull))
                            art.TipoCategoria.Id = (int)datos.Lector["idCategoria"];
                    }
                    if (!(datos.Lector["Marca"] is DBNull))
                    {
                        art.NombreMarca = new Marca();
                        art.NombreMarca.Descripcion = (string)datos.Lector["Marca"];
                        if (!(datos.Lector["idMarca"] is DBNull))
                            art.NombreMarca.Id = (int)datos.Lector["idMarca"];
                    }
                    

                    List<Imagen> imagenes;
                    imagenes = obtenerImagenes(art.Id);
                    if (imagenes.Count > 0)
                        art.UrlImagen = imagenes;
                    listaArt.Add(art);
                }
                return listaArt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        public List<Imagen> obtenerImagenes(int id)
        {
            List<Imagen> listadoImagenes = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select i.id,i.imagenUrl,a.Id articulo from imagenes i, articulos a where a.Id=i.IdArticulo");
            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                    int articulo = (int)datos.Lector["articulo"];
                    if (articulo == id)
                    {
                        Imagen imagen = new Imagen();
                        imagen.Id = (int)datos.Lector["id"];
                        imagen.IdArticulo = articulo;
                        imagen.Url = (string)datos.Lector["imagenUrl"];
                        listadoImagenes.Add(imagen);
                    }
                }
                return listadoImagenes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                obtenerIdMarcaYCategoria(nuevo);
                datos.SetConsulta("insert into articulos (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) values(@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@Precio)");
                datos.SetParametros("@Codigo", nuevo.Codigo);
                datos.SetParametros("@Nombre", nuevo.Nombre);
                datos.SetParametros("@Descripcion", nuevo.Descripcion);
                datos.SetParametros("@IdMarca", nuevo.NombreMarca.Id);
                datos.SetParametros("@IdCategoria", nuevo.TipoCategoria.Id);
                datos.SetParametros("@Precio", nuevo.Precio);
                datos.EjecutarAccion();
                AgregarImagenes(nuevo.UrlImagen);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }

        private void obtenerIdMarcaYCategoria(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select c.id IDCategoria, m.id IDMarca from categorias c, marcas m where m.Descripcion=@nombreMarca and c.Descripcion=@nombreCategoria");
            datos.SetParametros("@nombreMarca", nuevo.NombreMarca.Descripcion);
            datos.SetParametros("@nombreCategoria", nuevo.TipoCategoria.Descripcion);

            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                    nuevo.TipoCategoria.Id = (int)datos.Lector["IDCategoria"];
                    nuevo.NombreMarca.Id = (int)datos.Lector["IDMarca"];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        private void AgregarImagenes(List<Imagen> imagenes)
        {
            int id = obtenerUltimoIdArticulos();
            foreach (Imagen aux in imagenes)
            {
                AccesoDatos datos = new AccesoDatos();
                datos.SetConsulta("insert into imagenes (IdArticulo,ImagenUrl) values(@idArticulo,@UrlImagen)");
                datos.SetParametros("@idArticulo", id);
                datos.SetParametros("@UrlImagen", aux.Url);
                try
                {
                    datos.EjecutarAccion();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                finally
                {
                    datos.CerrarConexion();
                }
            }
            
        }

        private int obtenerUltimoIdArticulos()
        {
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select id from articulos");
            int id=0;
            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                    id = (int)datos.Lector["id"];
                }
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void eliminarArticulo (int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("delete from ARTICULOS where id = @id");
                datos.SetParametros("@id", id);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion(); 
            }
        }


        public void modificarArticulo(Articulo artModificado)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("update articulos set Codigo=@codigo,Nombre=@nombre,descripcion=@Descripcion,idMarca=@idMarca,idCategoria=@idCategoria,Precio=@precio  where id=@id");
                datos.SetParametros("@codigo",artModificado.Codigo);
                datos.SetParametros("@nombre",artModificado.Nombre);
                datos.SetParametros("@Descripcion",artModificado.Descripcion);
                datos.SetParametros("@idMarca", artModificado.NombreMarca.Id);
                datos.SetParametros("@idCategoria", artModificado.TipoCategoria.Id);
                datos.SetParametros("@precio", artModificado.Precio);
                datos.SetParametros("@id", artModificado.Id);

                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public List<Articulo> detalleArticulo(int id)
        {
            List<Articulo> listaArt = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("select a.id,a.Codigo,a.Nombre,a.Descripcion,a.Precio,c.Descripcion Tipo,m.Descripcion Marca,m.Id idMarca, c.Id idCategoria from articulos a left join categorias c on a.IdCategoria=c.Id inner join marcas m on a.IdMarca=m.Id");
            try
            {
                datos.Consulta_A_DB();
                while (datos.Lector.Read())
                {
                    int articulo = (int)datos.Lector["articulo"];

                    if (articulo == id)
                    {
                        Articulo art = new Articulo();
                        art.Id = (int)datos.Lector["id"];
                        art.Nombre = (string)datos.Lector["Nombre"];
                        art.Codigo = (string)datos.Lector["Codigo"];
                        art.Descripcion = (string)datos.Lector["Descripcion"];
                        art.Precio = (decimal)datos.Lector["Precio"];

                        if (!(datos.Lector["Tipo"] is DBNull))
                        {
                            art.TipoCategoria = new Categoria();
                            art.TipoCategoria.Descripcion = (string)datos.Lector["Tipo"];
                            if (!(datos.Lector["idCategoria"] is DBNull))
                                art.TipoCategoria.Id = (int)datos.Lector["idCategoria"];
                        }
                        if (!(datos.Lector["Marca"] is DBNull))
                        {
                            art.NombreMarca = new Marca();
                            art.NombreMarca.Descripcion = (string)datos.Lector["Marca"];
                            if (!(datos.Lector["idMarca"] is DBNull))
                                art.NombreMarca.Id = (int)datos.Lector["idMarca"];
                        }

                        List<Imagen> imagenes;
                        imagenes = obtenerImagenes(art.Id);
                        if (imagenes.Count > 0)
                            art.UrlImagen = imagenes;
                        listaArt.Add(art);
                    }
                }
                return listaArt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void InsertarImagenes(string url, int idArt)
        {

            AccesoDatos datos = new AccesoDatos();
            datos.SetConsulta("insert into imagenes (idArticulo,ImagenUrl) values(@idarticulo,@UrlImagen)");
            datos.SetParametros("@idarticulo", idArt);
            datos.SetParametros("@UrlImagen", url);
            try
            {
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }

        public void EliminarImagenes(string url, int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.SetConsulta("delete from imagenes where idArticulo=@id and ImagenUrl=@url");
                datos.SetParametros("@id", id);
                datos.SetParametros("@url", url);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
    }
}

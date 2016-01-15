using LinQ.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ
{
    class Program
    {
        private static void Main(string[] args)
        {
            Mostrar_Info();            
        }
        /// <summary>
        /// Funcion que muestra la informacion referente a la evaluacion de Linq.
        /// </summary>
        private static void Mostrar_Info()
        {
            #region Setea el Origen de Datos
            var cursos = new List<Curso> 
            { 
                new Curso { IdCurso = "1BTC", NombreCurso = "Primero Computacion" }, 
                new Curso { IdCurso = "2BTC", NombreCurso = "Segundo Computacion" }, 
                new Curso { IdCurso = "3BTC", NombreCurso = "Tercero Computacion" }, 
                new Curso { IdCurso = "4BTC", NombreCurso = "Cuarto Computacion" } 
            };

            // Origen de datos de estudiantes
            var estudiantes = new List<Estudiante> 
            { 
                new Estudiante {IdCurso="1BTC", Nombre="Josue", Apellido="Aleman", Id=111, Puntos= new List<int> {90, 92, 81, 60}}, 
                new Estudiante {IdCurso="2BTC", Nombre="Vinicio", Apellido="Watters", Id=112, Puntos= new List<int> {88, 84, 91, 40}}, 
                new Estudiante {IdCurso="2BTC", Nombre="Carlos", Apellido="Perez", Id=113, Puntos= new List<int> {17, 94, 65, 91}}, 
                new Estudiante {IdCurso="1BTC", Nombre="Jose", Apellido="Garcia", Id=114, Puntos= new List<int> {97, 89, 85, 72}},
                new Estudiante {IdCurso="3BTC", Nombre="Ana", Apellido="Garcia", Id=115, Puntos= new List<int> {93, 98, 91, 68}}, 
                new Estudiante {IdCurso="1BTC", Nombre="Franco", Apellido="Medina", Id=116, Puntos= new List<int> {99, 100, 90, 100}}, 
                new Estudiante {IdCurso="2BTC", Nombre="Alexis", Apellido="Rubio", Id=117, Puntos= new List<int> {88, 92, 80, 88}}, 
                new Estudiante {IdCurso="2BTC", Nombre="David", Apellido="Garcia", Id=118, Puntos= new List<int> {45, 90, 83, 17}}, 
                new Estudiante {IdCurso="1BTC", Nombre="Hugo", Apellido="Lopez", Id=119, Puntos= new List<int> {68, 79, 88, 18}}, 
                new Estudiante {IdCurso="3BTC", Nombre="Alonso", Apellido="Garcia", Id=120, Puntos= new List<int> {99, 82, 81, 100}}, 
                new Estudiante {IdCurso="1BTC", Nombre="Marco", Apellido="Lopez", Id=121, Puntos= new List<int> {96, 85, 91, 45}}, 
                new Estudiante {IdCurso="5BTC", Nombre="Tulio", Apellido="Meza", Id=122, Puntos= new List<int> {94, 92, 91, 91} } 
            };
            #endregion

            #region #1
            var consulta = from estudiante in estudiantes
                           where estudiante.Puntos[0] > 90
                           select estudiante;

            foreach (var e in consulta)
            {
                Console.WriteLine("Estudiante: {0}, Nota:{1}", e.Nombre, e.Puntos[0]);
            }
            #endregion

            #region #2
            var consulta2 = from estudiante in estudiantes
                           where estudiante.Puntos[0] > 90 && estudiante.Puntos[3] < 80
                           select estudiante;

            Console.Write("\n");
            foreach (var e in consulta2)
            {                
                Console.WriteLine("Estudiante: {0}, Nota:{1}", e.Nombre, e.Puntos[0]);
            }
            #endregion
                        
            #region #3
            var consultaAsc = from estudiante in estudiantes
                            orderby estudiante.Apellido
                            select estudiante;

            Console.Write("\n");
            foreach (var e in consultaAsc)
            {
                Console.WriteLine("Estudiante: {0}", e.Apellido);
            }
            #endregion

            #region #4
            var consultaDesc = from estudiante in estudiantes
                            orderby estudiante.Apellido descending
                            select estudiante;

            Console.Write("\n");
            foreach (var e in consultaDesc)
            {
                Console.WriteLine("Estudiante: {0}", e.Apellido);
            }
            #endregion

            #region #5
            var consultaGroup = from estudiante in estudiantes
                            group estudiante by estudiante.Apellido[0] into es
                            orderby es.Key
                            select es;

            Console.Write("\n");
            foreach (var eGroup in consultaGroup)
            {
                Console.WriteLine("{0}", eGroup.Key);

                foreach(var es in eGroup)
                {
                    Console.WriteLine(" {0}", es.Nombre);
                }
            }
            #endregion

            #region #6
            var consultaSum = from estudiante in estudiantes                            
                            select estudiante.Puntos.Sum(q => q);

            Console.Write("\n");
            foreach (var e in consultaSum)
            {
                Console.WriteLine("{0}", e);
            }
            #endregion

            #region #7
            var consultaProm = estudiantes.Average(n => n.Puntos.Sum(x => x));
            //var consultaProm = from estudiante in estudiantes
            //                   select estudiante.Puntos.Average(q => q);

            Console.WriteLine("\n{0}", consultaProm);
            #endregion

            #region #8
            var consultaFilter = from estudiante in estudiantes
                                 where estudiante.Apellido == "Lopez"
                                 select new { Apellido = estudiante.Apellido, Puntaje = estudiante.Puntos };
            //var consultaFilter = estudiantes.SelectMany(p => p.Puntos);
            
            Console.Write("\n");
            foreach (var eFilter in consultaFilter)
            {
                Console.WriteLine("Apellido:{0} \n {1}\n {2} \n {3} \n {4}", eFilter.Apellido,
                    eFilter.Puntaje[0], eFilter.Puntaje[1], eFilter.Puntaje[2], eFilter.Puntaje[3]);
            }
            #endregion

            #region #9
            //var consultaProm2 = estudiantes.Average(n => n.Puntos.Sum(x => x));
            var consultaPromEst = estudiantes.Where(n => n.Puntos.Sum() > Promedio_Clase(estudiantes));

            Console.WriteLine("\nPromedio General de la Clase: {0}", Promedio_Clase(estudiantes));
            foreach (var e in consultaPromEst)
            {
                Console.WriteLine("Studen ID: {0}, Score: {1}", e.Id, e.Puntos.Sum());
            }
            #endregion

            #region #10
            var consultaCali = from estudiante in estudiantes
                               select estudiante;

            Console.Write("\n");
            foreach (var e in consultaCali)
            {
                Console.WriteLine("Estudiante: {0}, Nota: {1}", e.Nombre, e.Puntos.Sum());
            }
            #endregion

            #region #11
            var consultaInner = from estudiante in estudiantes
                                join curso in cursos on estudiante.IdCurso equals curso.IdCurso
                                select new
                                {
                                    IdEstudiante = estudiante.Id,
                                    NombreEstudiante = estudiante.Nombre,
                                    ApellidoEstudiante = estudiante.Apellido,
                                    Promedio = estudiante.Puntos.Average(),
                                    IdCurso = curso.IdCurso,
                                    NombreCurso = curso.NombreCurso
                                };

            Console.Write("\n");
            foreach (var e in consultaInner)
            {
                Console.WriteLine("Id: {0}\nNombre: {1}\nApellido: {2}\nPromedio: {3}\nCurso: {4} - {5}\n\n", e.IdEstudiante, e.NombreEstudiante,
                                                                                    e.ApellidoEstudiante, e.Promedio, e.IdCurso, e.NombreCurso);
            }
            #endregion

            #region #12
            var consultaGroup2 = from curso in cursos
                                 join estudiante in estudiantes on curso.IdCurso equals estudiante.IdCurso
                                 group estudiante by estudiante.IdCurso into groupCurso
                                 select groupCurso;

            Console.Write("\n");
            foreach (var eGroup in consultaGroup2)
            {
                Console.WriteLine("{0}", eGroup.Key);

                foreach(var e in eGroup)
                {
                    Console.WriteLine(" {0}", e.Nombre);
                }
            }
            #endregion

            Console.ReadKey();
        }
        public static double Promedio_Clase(List<Estudiante> miEstudiante)
        {
            return miEstudiante.Average(n => n.Puntos.Sum(x => x));
        }
    }
}

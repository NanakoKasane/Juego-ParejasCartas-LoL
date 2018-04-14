using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//----------------------------------
using System.Windows.Threading;
using System.Threading;

namespace Juego_ParejaCartas
{
	/// <summary>
	/// Lógica de interacción para MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		// Lista de las imágenes para añadir:
		List<BitmapImage> listaImagenes = new List<BitmapImage>();

		// Lista de imágenes ya añadidas:
		List<Image> imagenesAnadidas = new List<Image>();

		// Lista de imágenes levantadas:
		List<Image> imagenesLevantadas = new List<Image>(); // Solo habrá 2 en la lista como máximo

		// Contador con el número de imágenes levantadas:
		static int contadorLevantadas = 0;

		// Contador de imágenes acertadas:
		static int contadorAcertadas = 0; // Si llega a 6 GANAS.


		public MainWindow()
		{
			InitializeComponent();
		}

		private void Grid_Loaded_1(object sender, RoutedEventArgs e)
		{
			// Se mostrará la imagen de la carta "por detrás"
			BitmapImage imagen = new BitmapImage(new Uri("./parejasImg/maxresdefault.jpg", UriKind.Relative));
			img_00.Source = imagen;
			img_01.Source = imagen;
			img_02.Source = imagen;
			img_10.Source = imagen;
			img_11.Source = imagen;
			img_12.Source = imagen;
			img_20.Source = imagen;
			img_21.Source = imagen;
			img_22.Source = imagen;
			img_30.Source = imagen;
			img_31.Source = imagen;
			img_32.Source = imagen;


			// Relleno la lista con las imágenes
			LlenarListaImagenes("./parejasImg/alistar.jpg");
			LlenarListaImagenes("./parejasImg/cait.jpg");
			LlenarListaImagenes("./parejasImg/galio.jpg");
			LlenarListaImagenes("./parejasImg/mf.jpg");
			LlenarListaImagenes("./parejasImg/sivir.jpg");
			LlenarListaImagenes("./parejasImg/xayahRakan.jpg");

			LlenarGridAlea();
		}


		private void LlenarListaImagenes(string ruta)
		{
			// Lista de BitmapImage donde se añadirán todas las imágenes:
			BitmapImage imagen = new BitmapImage(new Uri(ruta, UriKind.Relative));
			listaImagenes.Add(imagen);
			listaImagenes.Add(imagen);
		}


		private void LlenarGridAlea()
		{
			Random rnd = new Random();
			int contador = 1;

			// Cojo una posición aleatoria de la Lista de imágenes y la añado al Grid.
			for (int i = 0; i < grdRejilla.RowDefinitions.Count; i++)
			{
				for (int j = 0; j < grdRejilla.ColumnDefinitions.Count; j++)
				{
					// Coge la posición aleatoria de la lista de imágenes
					int posicion = rnd.Next(listaImagenes.Count);
					Image imagen = new Image(); // Creo una imagen 
					imagen.Source = listaImagenes[posicion];
					imagen.Stretch = Stretch.Fill;
					imagen.Visibility = System.Windows.Visibility.Hidden; // Primero son invisibles y luego si haces click se mostrará
					imagen.Name = "imagen0" + contador.ToString(); // Le añado el nombre a la imagen 
					contador++;

					// Borro esa imagen de la lista
					listaImagenes.RemoveAt(posicion);


					// Añado la imagen al Grid a la columna y fila por la que vaya rellenándose 
					grdRejilla.Children.Add(imagen);
					Grid.SetRow(imagen, i);
					Grid.SetColumn(imagen, j);

					imagenesAnadidas.Add(imagen);
				}
			}
		}


		// Juego en sí. Si clickas la imagen si hay otra clickada, comprueba si coincide con ella y si no, las vuelve a ocultar.
		private void Juego(int nCarta)
		{
			// Si no estaba ya levantada... La levanta
			if (imagenesAnadidas[nCarta].Visibility == System.Windows.Visibility.Hidden)
			{
				imagenesAnadidas[nCarta].Visibility = System.Windows.Visibility.Visible; // La muestra visible (levanta)
				++contadorLevantadas;

				imagenesLevantadas.Add(imagenesAnadidas[nCarta]);


				// Si aún solo hay una levantada, la deja levantada y ya está. Sale del método. No tiene que comparar aún con la otra carta
				if (contadorLevantadas == 1)
					return;



				// Si hay 2 cartas levantadas (a comparar) y no aciertas la carta (no son iguales las que comparas) se vuelve a esconder
				if (contadorLevantadas == 2 && imagenesAnadidas[nCarta].Source != imagenesLevantadas[0].Source)
				{
					MessageBox.Show("No ha acertado la pareja," + "\n" + "Siga intentándolo <3"); // Es muy cutre pero esto sirve de temporizador y se muestra para luego ocultarse xD

					// Se ocultan las 2 cartas:
					imagenesAnadidas[nCarta].Visibility = System.Windows.Visibility.Hidden;
					imagenesLevantadas[0].Visibility = System.Windows.Visibility.Hidden; // La primera con la que comparas siempre es la primera de la lista de cartas levantadas

					// Vacío la lista de Cartas Levantadas (Ya no hay ninguna levantada)
					imagenesLevantadas.Clear();
					contadorLevantadas = 0;

				}

				// Si has acertado (levantas 2 cartas iguales):
				else
				{
					contadorAcertadas++;

					// Vacío la lista de Cartas Levantadas 
					imagenesLevantadas.Clear();
					contadorLevantadas = 0;

					if (contadorAcertadas == 6)
						MessageBox.Show("¡GANASTE! <3");
				}

			}
		}

		#region Eventos de clickar la imagen

		private void img_00_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(0);
		}

		private void img_01_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(1);
		}

		private void img_02_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(2);
		}

		private void img_03_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(3);
		}

		private void img_04_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(4);
		}

		private void img_05_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(5);
		}

		private void img_06_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(6);
		}

		private void img_07_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(7);
		}

		private void img_08_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(8);
		}

		private void img_09_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(9);
		}

		private void img_10_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(10);
		}

		private void img_11_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Juego(11);
		}

		#endregion

	}
}


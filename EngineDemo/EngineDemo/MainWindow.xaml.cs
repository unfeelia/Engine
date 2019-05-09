using EngineDemo.Classes;
using EngineDemo.Classes.Models;
using EngineDemo.Classes.Models.ModelObjects;
using EngineDemo.Interfaces;
using EngineDemo.Interfaces.ModelObjectInterfaces;
using Newtonsoft.Json;
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

namespace EngineDemo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelDemo3 model;
        Hero hero1;
        Hero hero2;
        Hero choosen;

        public MainWindow()
        {
            hero1 = new Hero(new SimpleSquareCoordinate(1, 1, 0));
            hero1.SetOwnerID(1);
            hero2 = new Hero(new SimpleSquareCoordinate(10, 10, 0));
            hero2.SetOwnerID(2);

            choosen = hero1;

            model = new ModelDemo3(MapType.Square, 100, 100);
            model.SetMap(MapType.Square, 100, 100);
            var temp1 = new ResourceSet(new SimpleSquareCoordinate(1, 1, 1));
            model.AddResourceType("Gold");
            model.AddResourceType("Stone");
            model.AddPlayer();
            model.AddResourceType("Sera");
            model.AddPlayer();
            model.AddObject(hero1);
            model.AddObject(hero2);

            model.AddObjectType(typeof(IOwnable));
            model.AddObjectType(typeof(ICollectable));
            model.AddObjectType(typeof(IMobile));
            model.AddObjectType(typeof(IWall));

            var Temp1 = new ResourceSet(new SimpleSquareCoordinate(5, 5, 5));
            var Temp2 = new ResourceSet(new SimpleSquareCoordinate(7, 3, 5));
            var Temp3 = new ResourceSet(new SimpleSquareCoordinate(7, 5, 5));
            var Temp4 = new WallDemo(new SimpleSquareCoordinate(0, 3, 5));
            var Temp5 = new WallDemo(new SimpleSquareCoordinate(3, 0, 5));
            var Temp6 = new WallDemo(new SimpleSquareCoordinate(3, 3, 5));
            Shahta sh = new Shahta(new SimpleSquareCoordinate(9, 9, 0));

            Temp1.AddResource("Gold", 100);
            Temp1.AddResource("Sera", 4);
            Temp1.AddResource("Stone", 15);
            
            Temp2.AddResource("Gold", 123);
            Temp2.AddResource("Sera", 0);
            Temp2.AddResource("Stone", 2);

            Temp3.AddResource("Gold", 98);
            Temp3.AddResource("Sera", 12);
            Temp3.AddResource("Stone", 11);

            model.AddObject(Temp1);
            model.AddObject(Temp2);
            model.AddObject(Temp3);
            model.AddObject(Temp4);
            model.AddObject(Temp5);
            model.AddObject(Temp6);
            model.AddObject(sh);

            model.SetMapSize(50, 50);

            InitializeComponent();

            Draw();
        }

        public void Draw()
        {
            //Canvas canvas = this.canvas;
            canvas.Children.Clear();
            canvas.Background = Brushes.LightSteelBlue;
            canvas.Width = 800;
            canvas.Height = 400;
            // Add a "Hello World!" text element to the Canvas
            //TextBlock txt1 = new TextBlock();
            //txt1.FontSize = 14;
            //txt1.Text = "Hello World!";
            //Canvas.SetTop(txt1, 100);
            //Canvas.SetLeft(txt1, 10);
            //myCanvas.Children.Add(txt1);

            // Add a second text element to show how absolute positioning works in a Canvas
            //TextBlock txt2 = new TextBlock();
            //txt2.FontSize = 22;
            //txt2.Text = "Isn't absolute positioning handy?";
            //Canvas.SetTop(txt2, 200);
            //Canvas.SetLeft(txt2, 75);
            //myCanvas.Children.Add(txt2);

            foreach (var temp in model.GetObjects())
            {
                if(temp.Value.Disable)
                {
                    continue;
                }
                int x = temp.Value.GetPosition().GetX() * 10;
                int y = temp.Value.GetPosition().GetY() * 10;

                Rectangle redRectangle = new Rectangle();
                redRectangle.Width = 10;
                redRectangle.Height = 10;
                if(temp.Value is WallDemo)
                {
                    redRectangle.Stroke = new SolidColorBrush(Colors.Green);
                    redRectangle.Fill = new SolidColorBrush(Colors.Green);
                }
                else if (temp.Value is ResourceSet)
                {
                    redRectangle.Stroke = new SolidColorBrush(Colors.Gold);
                    redRectangle.Fill = new SolidColorBrush(Colors.Gold);
                }
                else if (temp.Value is Shahta)
                {
                    if (((Shahta)temp.Value).GetOwnerID() == 1)
                    {
                        redRectangle.Stroke = new SolidColorBrush(Colors.Red);
                        redRectangle.Fill = new SolidColorBrush(Colors.Red);
                    }
                    else
                    if (((Shahta)temp.Value).GetOwnerID() == 2)
                    {
                        redRectangle.Stroke = new SolidColorBrush(Colors.Blue);
                        redRectangle.Fill = new SolidColorBrush(Colors.Blue);
                    }
                    else
                    {
                        redRectangle.Stroke = new SolidColorBrush(Colors.Gray);
                        redRectangle.Fill = new SolidColorBrush(Colors.Gray);
                    }
                }
                else
                {
                    redRectangle.Stroke = new SolidColorBrush(Colors.Black);
                    redRectangle.Fill = new SolidColorBrush(Colors.Black);
                }
                redRectangle.StrokeThickness = 10;
                // Set Canvas position    
                Canvas.SetLeft(redRectangle, x);
                Canvas.SetTop(redRectangle, y);
                // Add Rectangle to Canvas    
                canvas.Children.Add(redRectangle);

            }
            int i = 0;
            foreach (var temp in model.GetPlayers())
            {
                int id = temp.Key;
                TextBlock txt1 = new TextBlock();
                txt1.FontSize = 14;
                txt1.Text = "Player " + i + " ";
                Canvas.SetTop(txt1, -txt1.FontSize * (i + 1));
                Canvas.SetLeft(txt1, 10);
                foreach (var res in temp.Value.GetResources())
                {
                    txt1.Text += res.Key + ": " + res.Value + " ";
                }
                canvas.Children.Add(txt1);
                ++i;
            }
            i = 0;
            foreach (var temp in model.GetResources())
            {
                string res = temp;
                TextBlock txt1 = new TextBlock();
                txt1.FontSize = 14;
                txt1.Text = res.ToString();
                Canvas.SetTop(txt1, -txt1.FontSize * 3);
                Canvas.SetLeft(txt1, 10 + 100 * i);
                canvas.Children.Add(txt1);
                ++i;
            }

            //this.a .AddChild(myCanvas);

            //this.Content = canvas;
            //this.Title = "Canvas Sample";
            //this.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ICoordinate coor = new SimpleSquareCoordinate(choosen.GetPosition().GetX(), choosen.GetPosition().GetY() + 1, choosen.GetPosition().GetZ());
            model.SetDestination(choosen, coor);
            model.Iteration();
            //model.AddPlayer();
            //model.AddResourceType("PineAplle");
            Draw();
        }

        private void Top_Click(object sender, RoutedEventArgs e)
        {
            ICoordinate coor = new SimpleSquareCoordinate(choosen.GetPosition().GetX(), choosen.GetPosition().GetY() - 1, choosen.GetPosition().GetZ());
            model.SetDestination(choosen, coor);
            model.Iteration();
            Draw();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {
            ICoordinate coor = new SimpleSquareCoordinate(choosen.GetPosition().GetX() - 1, choosen.GetPosition().GetY(), choosen.GetPosition().GetZ());
            model.SetDestination(choosen, coor);
            model.Iteration();
            Draw();
        }

        private void Rigth_Click(object sender, RoutedEventArgs e)
        {
            ICoordinate coor = new SimpleSquareCoordinate(choosen.GetPosition().GetX() + 1, choosen.GetPosition().GetY(), choosen.GetPosition().GetZ());
            model.SetDestination(choosen, coor);
            model.Iteration();
            Draw();
        }

        private void Skip_Click(object sender, RoutedEventArgs e)
        {
            //model.EndTurn(1);
            if (choosen == hero1)
            {
                choosen = hero2;
            }
            else
            {
                choosen = hero1;
            }
            Draw();
        }

        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            model.EndTurn(1);
            Draw();
            //if(choosen == hero1)
            //{
            //    choosen = hero2;
            //}
            //else
            //{
            //    choosen = hero1;
            //}
        }
    }
}

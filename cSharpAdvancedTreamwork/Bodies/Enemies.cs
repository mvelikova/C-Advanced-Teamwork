﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using cSharpAdvancedTreamwork.Conts;

namespace cSharpAdvancedTreamwork.Bodies
{
    public class Enemies
    {
        public struct Coordinates
        {
            public int x;
            public int y;

            public Coordinates(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        public Enemies()
        {
            shipEnemy = Constants.EnemyShipPicture;
            var rnd = new Random();
            var x = rnd.Next(2, Constants.PlayBoxWidth - Constants.EnemyShipWidth - 1);
            var y = 2;
            Position.x = x;
            Position.y = y;
        }

        public Coordinates Position;
        public string[] shipEnemy { get; set; }
        private bool killed = false;
        public void DrawShip()
        {

            Coordinates coordinates = this.Position;
            foreach (var line in shipEnemy)
            {
                Console.SetCursorPosition(coordinates.x, coordinates.y);
                Console.WriteLine(line);
                coordinates.y++;
            }
        }

        public bool CanBeSpawned(List<Enemies> enemies)
        {

            for (int i = 0; i < enemies.Count; i++)
            {
                var e = enemies[i];
                if (!((this.Position.x + Constants.EnemyShipWidth <= e.Position.x ||
                     this.Position.x >= e.Position.x + Constants.EnemyShipWidth) &&
                    (this.Position.y + Constants.EnemyShipHeight <= e.Position.y)))
                {
                    return false;
                }

            }
            return true;
        }
        public static void MoveEnemies(List<Enemies> e,ref int missed,ref bool updated)
        {
            updated = false;
            for (int i = 0; i < e.Count; i++)
            {
                if (e[i].Position.y == Constants.ConsoleWindowHeight - 7)
                {
                    Console.SetCursorPosition(e[i].Position.x, e[i].Position.y);
                    Console.WriteLine(new String(' ', 7));
                    Console.SetCursorPosition(e[i].Position.x, e[i].Position.y + 1);
                    Console.WriteLine(new String(' ', 7));
                    Console.SetCursorPosition(e[i].Position.x, e[i].Position.y + 2);
                    Console.WriteLine(new String(' ', 7));
                    missed++;
                    updated = true;
                    e.Remove(e[i]);
                }
                else
                {
                    Console.SetCursorPosition(e[i].Position.x, e[i].Position.y);
                    Console.WriteLine(new String(' ', 7));
                    e[i].Position.y++;
                    e[i].DrawShip();
                }
            }
        }

        public static bool CheckForCollision(List<Enemies> e, MainShip ship)
        {
            foreach (var enemy in e)
            {
                if ((enemy.Position.x >= ship.position.x && enemy.Position.x <= ship.position.x + 6)
                    || (enemy.Position.x + 6 >= ship.position.x && enemy.Position.x + 6 <= ship.position.x + 6))
                {
                    if (enemy.Position.y >= ship.position.y && enemy.Position.y <= ship.position.y + 2 ||
                        enemy.Position.y + 2 >= ship.position.y && enemy.Position.y + 3 <= ship.position.y + 2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Menekulj.Model;


namespace Menekulj.Persistance
{
    public struct SaveGameState
    {
        public Player Player { get; private set;}        
        public List<Enemy> Enemies { get; private set;}
        public Menekulj.Model.Cell[] Cells { get; private set; }
        public uint MineCount { get; private set; }
        public byte MatrixSize { get; private set; }

        [JsonConstructor]
        public SaveGameState(Player Player, List<Enemy> Enemies, Menekulj.Model.Cell[] Cells,  uint MineCount,byte MatrixSize)
        {
            this.Player = Player;
            this.Enemies = Enemies;
            this.Cells = Cells;
            this.MineCount = MineCount;

            this.MatrixSize= MatrixSize;
        }

        public SaveGameState(GameModel modelToSave)
        {
            this.Player = modelToSave.Player;
            this.Enemies = modelToSave.Enemies;
            this.Cells = new Menekulj.Model.Cell[modelToSave.MatrixSize*modelToSave.MatrixSize];

            for (int i = 0; i < modelToSave.MatrixSize; i++)
            {
                for (int j = 0; j < modelToSave.MatrixSize; j++)
                {
                    this.Cells[i *modelToSave.MatrixSize+ j] = modelToSave.GetCell(i,j);
                }
            }

            this.MineCount = modelToSave.MineCount;
            this.MatrixSize = modelToSave.MatrixSize;
        }
    }
}

using Menekulj.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Menekulj.Persistance
{
    public static class Persistance
    {
      

        public static async Task<GameModel> LoadStateAsync(string filePath)
        {
          SaveGameState state=  JsonSerializer.Deserialize<SaveGameState>(await File.ReadAllTextAsync(filePath));


            return new GameModel(state);
        }

        public static async Task SaveStateAsync(string fileName,GameModel modelToSave)
        {
            SaveGameState state = new SaveGameState(modelToSave);

            string jsonString = JsonSerializer.Serialize(state);

            await File.WriteAllTextAsync(fileName,jsonString);
        }


    }
}

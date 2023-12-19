
using System.Collections.Generic;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // Можно удалить этот код, но тогда удалите и демо (папка Example)
        //Saves?
        public PlayerSkin skin;
        public DragonType enemy;
        public int gold = 0;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны
        public Dragon NowDragon;
        public Character NowCharacter;
        public enum PlayerSkin
        {
            Wanderer = 0,
            Cliric = 1,
            Piromant = 2,
        }

        public enum DragonType
        {
            Vivern = 0,
            SwampDragon = 1,
            MountainDragon = 2,
        }

        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            //openLevels[1] = true;
            //for(var i = 0; i < NowCharacter.SkillLevel.Count; i++)
            //{
            //    NowCharacter.SkillLevel[i] = 1;
            //}
        }
    }
}

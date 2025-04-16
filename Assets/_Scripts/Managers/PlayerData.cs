namespace AlienInvasion
{
    [System.Serializable]
    public class PlayerData
    {
        public int money;
        public int healthUpgrades;
        public int ammoCapacityUpgrades;

        public PlayerData(int money, int healthUpgrades, int ammoCapacityUpgrades)
        {
            this.money = money;
            this.healthUpgrades = healthUpgrades;
            this.ammoCapacityUpgrades = ammoCapacityUpgrades;
        }

        public PlayerData()
        {
            this.money = 0;
            this.healthUpgrades = 0;
            this.ammoCapacityUpgrades = 0;
        }
    }
}

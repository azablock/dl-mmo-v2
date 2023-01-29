namespace _Darkland.Sources.Models.Equipment {

    public interface IWearable {

        WearableSlot WearableItemSlot { get; }
        WearableType WearableItemType { get; }

    }

}
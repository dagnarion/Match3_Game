using System;

public enum ItemType {
    UnB = -2,
    UnA = -1,
    None = 0,
    A = 1,
    B = 2
}

public class ItemModel {
    public ItemType Type;
    public ItemModel(ItemType type) { Type = type; }
    public void SwapType(ItemType t) { Type = t; }
}

public class Program {
    public static void Main() {
        int rows = 3;
        int cols = 3;
        ItemModel[,] grid = new ItemModel[rows, cols];
        
        // AAA
        // ABA
        // AAA
        grid[0,0] = new ItemModel(ItemType.A); grid[0,1] = new ItemModel(ItemType.A); grid[0,2] = new ItemModel(ItemType.A);
        grid[1,0] = new ItemModel(ItemType.A); grid[1,1] = new ItemModel(ItemType.B); grid[1,2] = new ItemModel(ItemType.A);
        grid[2,0] = new ItemModel(ItemType.A); grid[2,1] = new ItemModel(ItemType.A); grid[2,2] = new ItemModel(ItemType.A);
        
        // Match logic
        for(int r = 0; r < rows; r++)
        for (int c = 0; c < cols - 2; c++) {
            int cell1 = Math.Abs((int)grid[r, c].Type);
            int cell2 = Math.Abs((int)grid[r, c+1].Type);
            int cell3 = Math.Abs((int)grid[r, c+2].Type);
            if (cell1 == cell2 && cell2 == cell3 && cell1 != 0) {
                grid[r,c].SwapType((ItemType) (-cell1));
                grid[r,c+1].SwapType((ItemType) (-cell1));
                grid[r,c+2].SwapType((ItemType) (-cell1));
            }
        }
        
        for(int c = 0; c < cols; c++)
        for (int r = 0; r < rows - 2; r++) {
            int cell1 = Math.Abs((int)grid[r, c].Type);
            int cell2 = Math.Abs((int)grid[r+1, c].Type);
            int cell3 = Math.Abs((int)grid[r+2, c].Type);
            if (cell1 == cell2 && cell2 == cell3 && cell1 != 0) {
                grid[r,c].SwapType((ItemType) (-cell1));
                grid[r+1,c].SwapType((ItemType) (-cell1));
                grid[r+2,c].SwapType((ItemType) (-cell1));
            }
        }
        
        for(int r = 0; r < rows; r++) {
            for (int c = 0; c < cols; c++) {
                Console.Write(grid[r,c].Type + " \t");
            }
            Console.WriteLine();
        }
    }
}

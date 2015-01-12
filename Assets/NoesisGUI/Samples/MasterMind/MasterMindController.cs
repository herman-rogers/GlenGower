using UnityEngine;
using System;
using System.Collections;

public class MasterMindController : MonoBehaviour 
{
    private static int ColumnsCount = 4;
    private static int ColorsCount = 8;
    private static int RowsCount = 10;

    private Noesis.FrameworkElement _root;
    private Noesis.Grid[] _grids;
    private Noesis.Border[] _borders;
    private Noesis.Button[] _chkButtons;
    private Noesis.TextBlock _status;
    private Noesis.RadioButton[] _colors;
    private Noesis.Ellipse[] _solutions;
    private Noesis.TextBlock[] _solutionTexts;
    private Noesis.Button _restartGame;
    
    private static string [] ColorRBNames = new string []
    {
        "RedRB",
        "BlueRB",
        "GreenRB",
        "YellowRB",
        "BrownRB",
        "PurpleRB",
        "OrangeRB",
        "PinkRB"
    };
    
    private class Row
    {
        public Row()
        {
            _status = new int[MasterMindController.ColumnsCount];
            for (int i = 0; i < MasterMindController.ColumnsCount; ++i)
            {
                _status[i] = -1;
            }

            _btn = new Noesis.ToggleButton[MasterMindController.ColumnsCount];

            _sol = new Noesis.Ellipse[MasterMindController.ColumnsCount];
        }
        
        public int [] _status;
        public Noesis.ToggleButton[] _btn;
        public Noesis.Ellipse[] _sol;
    };
    
    private Row [] _rows;
    
    private int _currentRow;
    
    private int _currentColor;

    private Noesis.Brush _blackCircleBrush;
    private Noesis.Brush _whiteCircleBrush;
    private Noesis.Brush _blackSolidBrush;
    private Noesis.Brush _solutionBrush;
    
    private int [] _colorKey;
    
    private System.Random _random;
        
    ////////////////////////////////////////////////////////////////////////////////////////////
    // Use this for initialization
    void Start () 
    {
        // Access to the NoesisGUI component
        NoesisGUIPanel noesisGUI = GetComponent<NoesisGUIPanel>();
        
        // Obtain the root of the loaded UI resource, in this case it is a Grid element
        _root = noesisGUI.GetContent();
        
        // Grids & Borders
        _grids = new Noesis.Grid[MasterMindController.RowsCount];
        _borders = new Noesis.Border[MasterMindController.RowsCount];
        for (int i = 0; i < MasterMindController.RowsCount; ++i)
        {
            _grids[i] = (Noesis.Grid)_root.FindName(String.Format("Bd{0}", i));
            if (_grids[i] == null)
            {
                Debug.LogError("Grid " + i.ToString() + " not found");
            }
            _borders[i] = (Noesis.Border)_root.FindName(String.Format("Bg{0}", i));
            if (_borders[i] == null)
            {
                Debug.LogError("Border " + i.ToString() + " not found");
            }
        }
        
        // Check buttons
        _chkButtons = new Noesis.Button[MasterMindController.RowsCount];
        for (int i = 0; i < MasterMindController.RowsCount; ++i)
        {
            _chkButtons[i] = (Noesis.Button)_root.FindName(String.Format("ChkBtn{0}", i));
            _chkButtons[i].Click += this.OnCheckClick;
            
        }
        
        // Status text
        _status = (Noesis.TextBlock)_root.FindName("StatusText");
        
        // Rows
        _rows = new MasterMindController.Row[MasterMindController.RowsCount];
        
        for (int r = 0; r < MasterMindController.RowsCount; ++r)
        {
            _rows[r] = new MasterMindController.Row();
            for (int x = 0; x < MasterMindController.ColumnsCount; ++x)
            {
                _rows[r]._btn[x] = (Noesis.ToggleButton)_root.FindName(String.Format("Pos{0}{1}", r, x));
                _rows[r]._btn[x].Click += this.OnToggleClick;
                _rows[r]._sol[x] = (Noesis.Ellipse)_root.FindName(String.Format("Sol{0}{1}", r, x));
            }
        }
        
        // Current color red
        _currentColor = 0;
        
        // Colors
        _colors = new Noesis.RadioButton[MasterMindController.ColorsCount];
        
        for (int i = 0; i < 8; ++i)
        {
            _colors[i] = (Noesis.RadioButton)_root.FindName(MasterMindController.ColorRBNames[i]);
            _colors[i].Checked += this.OnColorCheck;
        }
        
        // Get the solution ellipses
        _solutions = new Noesis.Ellipse[MasterMindController.ColumnsCount];
        _solutionTexts = new Noesis.TextBlock[MasterMindController.ColumnsCount];
        for (int i = 0; i < MasterMindController.ColumnsCount; ++i)
        {
            _solutions[i] = (Noesis.Ellipse)_root.FindName(String.Format("Key{0}", i));
            _solutionTexts[i] = (Noesis.TextBlock)_root.FindName(String.Format("KeyText{0}", i));
        }
        
        // Restart game
        _restartGame = (Noesis.Button)_root.FindName("btnRestart");
        _restartGame.Click += this.OnRestartGame;
            
        // Get the brushes
        _blackCircleBrush = (Noesis.Brush)_root.Resources["BlackCircleBrush"];
        _whiteCircleBrush = (Noesis.Brush)_root.Resources["WhiteCircleBrush"];
        _blackSolidBrush = (Noesis.Brush)_root.Resources["BlackColorBrush"];
        _solutionBrush = (Noesis.Brush)_root.Resources["SolutionBrush"];
        
        _random = new System.Random();
        
        this.StartGame();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void StartGame()
    {
        // Create the color key
        _colorKey = new int[MasterMindController.ColumnsCount];
        for (int x = 0; x < MasterMindController.ColumnsCount; ++x)
        {
            _colorKey[x] = _random.Next(MasterMindController.ColorsCount);
        }
        
        // print (String.Format ("Color key: {0} {1} {2} {3}", mColorKey[0], mColorKey[1], mColorKey[2], mColorKey[3]));
        
        // First row
        _currentRow = MasterMindController.RowsCount - 1;
        
        // Set the initial text
        _status.Text = "Lets play!";
        
        // Hide the solution
        for (int i = 0; i < MasterMindController.ColumnsCount; ++i)
        {
            _solutions[i].Fill = _solutionBrush;
            _solutionTexts[i].Visibility = Noesis.Visibility.Visible;
        }
        
        // Reset borders
        for (int i = 0; i < MasterMindController.RowsCount; ++i)
        {
            for (int x = 0; x < MasterMindController.ColumnsCount; ++x)
            {
                _rows[i]._status[x] = -1;
                _rows[i]._btn[x].Background = _blackSolidBrush;
                _rows[i]._sol[x].Fill = _blackSolidBrush;
            }
            
            // Disable row
            this.DisableRow(i);
        }        
        
        // Enable current row
        EnableRow(_currentRow);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void DisableRow(int i)
    {
        // Border
        _grids[i].IsEnabled = false;
        _borders[i].Visibility = Noesis.Visibility.Hidden;
        
        // Button
        _chkButtons[i].Visibility = Noesis.Visibility.Hidden;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void EnableRow(int i)
    {
        // Border
        _grids[i].IsEnabled = true;
        _borders[i].Visibility = Noesis.Visibility.Visible;
        
        // Button
        _chkButtons[i].Visibility = Noesis.Visibility.Visible;
        _chkButtons[i].IsEnabled = false;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnCheckClick(object sender, Noesis.RoutedEventArgs a)
    {
        this.DisableRow(_currentRow);
        
        if (this.Check(_currentRow))
        {
            this.FinishGame(true);
            return;
        }
        
        
        if (_currentRow > 0)
        {
            --_currentRow;
            this.EnableRow(_currentRow);
        }
        else
        {
            this.FinishGame(false);
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnColorCheck(object sender, Noesis.RoutedEventArgs a)
    {
        Noesis.FrameworkElement f = sender as Noesis.FrameworkElement;
        string name = f.Name;
        
        _currentColor = Array.IndexOf(MasterMindController.ColorRBNames, name);
    }

    void OnToggleClick(object sender, Noesis.RoutedEventArgs a)
    {
        Noesis.FrameworkElement f = sender as Noesis.FrameworkElement;
        string name = f.Name;
        name = name.Replace("Pos", "");
        
        int pos = int.Parse(name) % 10;
        
        _rows[_currentRow]._status[pos] = 
            this.SetColor(_rows[_currentRow]._btn[pos], pos);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    int SetColor(Noesis.ToggleButton btn, int x)
    {
        if (_currentColor < 0)
        {
            return - 1;
        }
        
        btn.Background = _colors[_currentColor].Background;
        
        bool enabled = true;
        
        for (int i = 0; i < MasterMindController.ColumnsCount && enabled; ++i)
        {
            if (i == x)
            {
                continue;
            }
            enabled = _rows[_currentRow]._status[i] >= 0;
        }
        
        _chkButtons[_currentRow].IsEnabled = enabled;
        
        return _currentColor;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    bool Check(int r)
    {
        int numWhites = 0;
        int numBlacks = 0;
        
        bool [] solFound =
        {
            false, false, false, false
        };
        
        // Compute the number of black solution balls
        for (int x = 0; x < MasterMindController.ColumnsCount; ++x)
        {
            if (_rows[r]._status[x] == _colorKey[x])
            {
                ++numBlacks;
                solFound[x] = true;
            }
        }
        
        bool [] useForWhite = {false, false, false, false};
        
        // Compute the number of white solution balls
        for (int x = 0; x < MasterMindController.ColumnsCount; ++x)
        {
            if (solFound[x])
            {
                continue;
            }
            
            for (int j = 0; j < MasterMindController.ColumnsCount; ++j)
            {
                if (j == x || solFound[j] || useForWhite[j])
                {
                    continue;
                }
                
                if (_rows[r]._status[x] == _colorKey[j])
                {
                    ++numWhites;
                    useForWhite[j] = true;
                    break;
                }
            }
        }
        
        for (int i = 0; i < numBlacks; ++i)
        {
            _rows[r]._sol[i].Fill = _blackCircleBrush;
        }
        for (int i = 0; i < numWhites; ++i)
        {
            _rows[r]._sol[numBlacks + i].Fill = _whiteCircleBrush;
        }
        
        return numBlacks == MasterMindController.ColumnsCount;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void FinishGame(bool win)
    {
        if (win)
        {
            _status.Text = "You win!";
        }
        else
        {
            _status.Text = "You lose...";
        }
        
        // Show the key color
        for (int i = 0; i < MasterMindController.ColumnsCount; ++i)
        {
            _solutions[i].Fill = _colors[_colorKey[i]].Background;
            _solutionTexts[i].Visibility = Noesis.Visibility.Hidden;
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    void OnRestartGame(object sender, Noesis.RoutedEventArgs a)
    {
        StartGame();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    void Update () 
    {
    
    }
}

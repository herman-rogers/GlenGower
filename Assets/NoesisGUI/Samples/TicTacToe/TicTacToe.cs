using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class TicTacToe : MonoBehaviour 
{
    Noesis.FrameworkElement _root;
    Noesis.FrameworkElement _boardPanel;
    Noesis.TextBlock _statusText;

    Noesis.TextBlock _scorePlayer1Text;
    Noesis.TextBlock _scorePlayer2Text;
    Noesis.TextBlock _scoreTiesText;
    Noesis.TextBlock _scoreText;

    Noesis.Storyboard _winAnimation;
    Noesis.Storyboard _tieAnimation;
    Noesis.Storyboard _resetAnimation;
    Noesis.Storyboard _progressAnimation;
    Noesis.Storyboard _progressFadeAnimation;
    Noesis.Storyboard _scoreHalfAnimation;
    Noesis.Storyboard _scoreEndAnimation;
    Noesis.Storyboard _statusHalfAnimation;
    Noesis.Storyboard _statusEndAnimation;

    Noesis.DependencyObject _winAnim0;
    Noesis.DependencyObject _winAnim1;
    Noesis.DependencyObject _winAnim2;

    Noesis.DependencyObject _scoreHalfAnim0;

    Noesis.DependencyObject _scoreEndAnim0;
    Noesis.DependencyObject _scoreEndAnim1;
    Noesis.DependencyObject _scoreEndAnim2;
    
    string _statusMsg;

    uint _scorePlayer1;
    uint _scorePlayer2;
    uint _scoreTies;
    uint _score;

    enum Player
    {
        Player_None,
        Player_1,
        Player_2
    };

    Player _player;
    Player _lastStarterPlayer;

    public class Cell
    {
        public string Player
        {
            set { _player = value; }
            get { return _player; }
        }
        private string _player;

        public Noesis.ToggleButton Btn
        {
            set { _btn = value; }
            get { return _btn; }
        }
        private Noesis.ToggleButton _btn;
    };

    private Cell [][] _board;

    // Use this for initialization
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void Start () 
    {
        // Access to the NoesisGUIPanel component
        NoesisGUIPanel noesisGUI = GetComponent<NoesisGUIPanel>();

        _root = noesisGUI.GetContent();

        _boardPanel = (Noesis.FrameworkElement)_root.FindName("Board");
        _boardPanel.MouseLeftButtonDown += this.BoardClicked;
        
        _board = new Cell [][]
        {
            new Cell[]
            {
                new Cell(),
                new Cell(),
                new Cell()
            },
            new Cell[]
            {
                new Cell(),
                new Cell(),
                new Cell()
            },
            new Cell[]
            {
                new Cell(),
                new Cell(),
                new Cell()
            }
        };
        
        for (int row = 0; row < 3; ++row)
        {
            for (int col = 0; col < 3; ++col)
            {
                string cellName = String.Format("Cell{0}{1}", row, col);

                _board[row][col].Btn = (Noesis.ToggleButton)_root.FindName(cellName);
                _board[row][col].Btn.IsEnabled = false;
                _board[row][col].Btn.Tag = _board[row][col];
                _board[row][col].Btn.Checked += this.CellChecked;
            }
        }

        _statusText = (Noesis.TextBlock)_root.FindName("StatusText");
        _scorePlayer1Text = (Noesis.TextBlock)_root.FindName("ScorePlayer1");
        _scorePlayer2Text = (Noesis.TextBlock)_root.FindName("ScorePlayer2");
        _scoreTiesText = (Noesis.TextBlock)_root.FindName("ScoreTies");
        _scoreText = null;

        _winAnimation = (Noesis.Storyboard)_root.Resources["WinAnim"];
        _winAnimation.Completed += this.WinAnimationCompleted;

        _winAnim0 = (Noesis.DependencyObject)_root.FindName("WinAnim0");
        _winAnim1 = (Noesis.DependencyObject)_root.FindName("WinAnim1");
        _winAnim2 = (Noesis.DependencyObject)_root.FindName("WinAnim2");

        _tieAnimation = (Noesis.Storyboard)_root.Resources["TieAnim"];
        _tieAnimation.Completed += this.TieAnimationCompleted;

        _resetAnimation = (Noesis.Storyboard)_root.Resources["ResetAnim"];
        _resetAnimation.Completed += this.ResetAnimationCompleted;

        _progressAnimation = (Noesis.Storyboard)_root.Resources["ProgressAnim"];

        _progressFadeAnimation = (Noesis.Storyboard)_root.Resources["ProgressFadeAnim"];
        _progressFadeAnimation.Completed += this.ProgressFadeAnimationCompleted;

        _scoreHalfAnimation = (Noesis.Storyboard)_root.Resources["ScoreHalfAnim"];
        _scoreHalfAnimation.Completed += this.ScoreHalfAnimationCompleted;

        _scoreHalfAnim0 = (Noesis.DependencyObject)_root.FindName("ScoreHalfAnim0");

        _scoreEndAnimation = (Noesis.Storyboard)_root.Resources["ScoreEndAnim"];

        _scoreEndAnim0 = (Noesis.DependencyObject)_root.FindName("ScoreEndAnim0");
        _scoreEndAnim1 = (Noesis.DependencyObject)_root.FindName("ScoreEndAnim1");
        _scoreEndAnim2 = (Noesis.DependencyObject)_root.FindName("ScoreEndAnim2");

        _statusHalfAnimation = (Noesis.Storyboard)_root.Resources["StatusHalfAnim"];
        _statusHalfAnimation.Completed += this.StatusHalfAnimationCompleted;

        _statusEndAnimation = (Noesis.Storyboard)_root.Resources["StatusEndAnim"];

        _statusText.Text = "Player 1 Turn";
        _player = Player.Player_1;
        _lastStarterPlayer = Player.Player_1;
        _scorePlayer1 = 0;
        _scorePlayer2 = 0;
        _scoreTies = 0;
        _score = 0;
    
        this.StartGame();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    // Update is called once per frame
    void Update () 
    {    
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void BoardClicked(object sender, Noesis.MouseButtonEventArgs e)
    {            
        if (_player == Player.Player_None)
        {
            if (_lastStarterPlayer == Player.Player_1)
            {
                _player = Player.Player_2;
                _lastStarterPlayer = Player.Player_2;
                _statusMsg = "Player 2 Turn";
            }
            else
            {
                _player = Player.Player_1;
                _lastStarterPlayer = Player.Player_1;
                _statusMsg = "Player 1 Turn";
            }
    
            _resetAnimation.Begin(_root);
            _statusHalfAnimation.Begin(_root);
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void CellChecked(object sender, Noesis.RoutedEventArgs e)
    {
        Noesis.FrameworkElement fe = (Noesis.FrameworkElement)sender;
        Cell cell = (Cell)fe.Tag;
    
        this.MarkCell(cell);
    
        string winPlay = "";
        if (this.HasWon(ref winPlay))
        {
            this.WinGame(winPlay);
        }
        else if (this.HasTied())
        {
            this.TieGame();
        }
        else
        {
            this.SwitchPlayer();
        }
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void WinAnimationCompleted(object arg0, Noesis.TimelineEventArgs arg1)
    {
        if (_player == Player.Player_1)
        {
            _scoreText = _scorePlayer1Text;
            _score = ++_scorePlayer1;
            this.UpdateScoreAnimation("ScorePlayer1");
        }
        else
        {
            _scoreText = _scorePlayer2Text;
            _score = ++_scorePlayer2;
            this.UpdateScoreAnimation("ScorePlayer2");
        }
    
        _player = Player.Player_None;
    
        _scoreHalfAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void TieAnimationCompleted(object arg0, Noesis.TimelineEventArgs arg1)
    {
        _scoreText = _scoreTiesText;
        _score = ++_scoreTies;
        this.UpdateScoreAnimation("ScoreTies");
    
        _player = Player.Player_None;
    
        _scoreHalfAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void ResetAnimationCompleted(object arg0, Noesis.TimelineEventArgs arg1)
    {
        this.StartGame();
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void ProgressFadeAnimationCompleted(object sender, Noesis.TimelineEventArgs args)
    {
        _progressAnimation.Stop(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void ScoreHalfAnimationCompleted(object arg0, Noesis.TimelineEventArgs arg1)
    {
        _scoreText.Text = String.Format("{0}", _score);
        _scoreEndAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void StatusHalfAnimationCompleted(object arg0, Noesis.TimelineEventArgs arg1)
    {
        _statusText.Text = _statusMsg;
        _statusEndAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    string GetPlayerState()
    {
        return _player == Player.Player_1 ? "Player1" : "Player2";
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void StartGame()
    {
        string player = this.GetPlayerState();

        Noesis.PlaneProjection projection = (Noesis.PlaneProjection)_boardPanel.Projection;
        projection.ClearAnimation(Noesis.PlaneProjection.RotationYProperty);
        Noesis.CompositeTransform t = (Noesis.CompositeTransform)_boardPanel.RenderTransform;
        t.ClearAnimation(Noesis.CompositeTransform.ScaleXProperty);
        t.ClearAnimation(Noesis.CompositeTransform.ScaleYProperty);

        for (int row = 0; row < 3; ++row)
        {
            for (int col = 0; col < 3; ++col)
            {
                _board[row][col].Player = "";
                _board[row][col].Btn.ClearAnimation(Noesis.UIElement.OpacityProperty);
                _board[row][col].Btn.IsEnabled = true;

                _board[row][col].Btn.IsChecked = false;
                Noesis.VisualStateManager.GoToState(_board[row][col].Btn, player, false);
            }
        }
        
        _progressAnimation.Begin(_root, _root, true);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void WinGame(string winPlay)
    {
        for (int row = 0; row < 3; ++row)
        {
            for (int col = 0; col < 3; ++col)
            {
                _board[row][col].Btn.IsEnabled = false;
            }
        }
    
        _statusMsg = String.Format("Player {0} Wins", _player == Player.Player_1 ? 1 : 2);

        Noesis.Storyboard.SetTargetName(_winAnim0, winPlay);

        Noesis.Storyboard.SetTargetName(_winAnim1, winPlay);

        Noesis.Storyboard.SetTargetName(_winAnim2, winPlay);
    
        _progressFadeAnimation.Begin(_root);
        _winAnimation.Begin(_root);
        _statusHalfAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void TieGame()
    {
        _statusMsg = "Game Tied";
    
        _progressFadeAnimation.Begin(_root);
        _tieAnimation.Begin(_root);
        _statusHalfAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void SwitchPlayer()
    {
        if (_player == Player.Player_1)
        {
            _player = Player.Player_2;
            _statusMsg = "Player 2 Turn";
        }
        else
        {
            _player = Player.Player_1;
            _statusMsg = "Player 1 Turn";
        }
    
        string player = this.GetPlayerState();
        for (int row = 0; row < 3; ++row)
        {
            for (int col = 0; col < 3; ++col)
            {
                TicTacToe.Cell cell = _board[row][col];
                if (cell.Player == "") 
                {
                    Noesis.VisualStateManager.GoToState(cell.Btn, player, false);
                }
            }
        }
    
        _statusHalfAnimation.Begin(_root);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void MarkCell(Cell cell)
    {
        string player = this.GetPlayerState();
    
        cell.Player = player;
        cell.Btn.IsEnabled = false;
        Noesis.VisualStateManager.GoToState(cell.Btn, player, false);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    void UpdateScoreAnimation(string targetName)
    {
        // Score Half
        Noesis.Storyboard.SetTargetName(_scoreHalfAnim0, targetName);
    
        // Score End
        Noesis.Storyboard.SetTargetName(_scoreEndAnim0, targetName);
        Noesis.Storyboard.SetTargetName(_scoreEndAnim1, targetName);
        Noesis.Storyboard.SetTargetName(_scoreEndAnim2, targetName);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool HasWon(ref string winPlay)
    {
        string player = GetPlayerState();
    
        for (int i = 0; i < 3; ++i)
        {
            if (this.PlayerCheckedRow(player, i))
            {
                winPlay = String.Format("WinRow{0}", i + 1);
                return true;
            }
    
            if (this.PlayerCheckedCol(player, i))
            {
                winPlay = String.Format("WinCol{0}", i + 1);
                return true;
            }
        }
    
        if (this.PlayerCheckedDiag(player, 0, 2))
        {
            winPlay = "WinDiagLR";
            return true;
        }
        
        if (this.PlayerCheckedDiag(player, 2, 0))
        {
            winPlay = "WinDiagRL";
            return true;
        }
    
        return false;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool HasTied()
    {
        for (int row = 0; row < 3; ++row)
        {
            for (int col = 0; col < 3; ++col)
            {
                if (_board[row][col].Player == "")
                {
                    return false;
                }
            }
        }
    
        return true;
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool PlayerCheckedRow(string player, int row)
    {
        return this.PlayerCheckedCell(player, row, 0) && this.PlayerCheckedCell(player, row, 1) &&
            this.PlayerCheckedCell(player, row, 2);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool PlayerCheckedCol(string player, int col)
    {
        return this.PlayerCheckedCell(player, 0, col) && this.PlayerCheckedCell(player, 1, col) &&
            this.PlayerCheckedCell(player, 2, col);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool PlayerCheckedDiag(string player, int start, int end)
    {
        return this.PlayerCheckedCell(player, start, 0) && this.PlayerCheckedCell(player, 1, 1) &&
            this.PlayerCheckedCell(player, end, 2);
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    bool PlayerCheckedCell(string player, int row, int col)
    {
        return _board[row][col].Player == player;
    }
}

local status = ChessConst.BattleStatusType.Start

function GameController.Init()
	ChessUtil.Log("GameController")
	GameController.RegisterEvents()

    ChessUtil.SendEvent(EVENT.CHESS_STAGE_START)
end

function GameController.RegisterEvents()
	ChessUtil.RegisterEvent(EVENT.CHESS_STAGE_START, GameController.ChessStageStart)
	ChessUtil.RegisterEvent(EVENT.CHESS_STAGE_PICK_HERO, GameController.ChessStagePickHero)
	ChessUtil.RegisterEvent(EVENT.CHESS_STAGE_PICK_PIECES, GameController.ChessStagePickPieces)
	ChessUtil.RegisterEvent(EVENT.CHESS_STAGE_BATTLE, GameController.ChessStageBattle)
	ChessUtil.RegisterEvent(EVENT.CHESS_STAGE_END, GameController.ChessStageEnd)
end

function GameController.UnregisterEvents()
	ChessUtil.UnregisterEvent(EVENT.CHESS_STAGE_START, GameController.ChessStageStart)
	ChessUtil.UnregisterEvent(EVENT.CHESS_STAGE_PICK_HERO, GameController.ChessStagePickHero)
	ChessUtil.UnregisterEvent(EVENT.CHESS_STAGE_PICK_PIECES, GameController.ChessStagePickPieces)
	ChessUtil.UnregisterEvent(EVENT.CHESS_STAGE_BATTLE, GameController.ChessStageBattle)
	ChessUtil.UnregisterEvent(EVENT.CHESS_STAGE_END, GameController.ChessStageEnd)
end

-- todoChess 展示系统的ip连接，初步完成连接
-- todoChess 选棋子阶段与战斗阶段的轮转
-- todoChess 干预英雄、棋子的抽取，干预战斗过程，干预战斗匹配
-- 场景层级，场景元素：地板、镜头、棋子、英雄

function GameController.ChessStageStart()
	ChessUtil.Log("GameController ChessStageStart")
	status = ChessConst.BattleStatusType.Start

	-- 玩家准备
	if not PlayerController.IsInit() then
		PlayerController.Init()
	end
end

function GameController.ChessStagePickHero()
	ChessUtil.Log("GameController ChessStagePickHero")
	status = ChessConst.BattleStatusType.PickHero

	-- 棋子、英雄准备
	if not PiecesController.IsInit() then
		PiecesController.Init()
	end
	if not HeroController.IsInit() then
		HeroController.Init()
	end
end

function GameController.ChessStagePickPieces()
	ChessUtil.Log("GameController ChessStagePickPieces")
	status = ChessConst.BattleStatusType.PickPieces

	-- 酒馆准备
	if not BartenderController.IsInit() then
		BartenderController.Init()
	else
		BartenderController.GenerateAllPlayersPiecesList()
	end
end

function GameController.ChessStageBattle()
	ChessUtil.Log("GameController ChessStageBattle")
	status = ChessConst.BattleStatusType.Battle

	-- 战斗准备
	if not BattleController.IsInit() then
		BattleController.Init()
	else
		BattleController.StartBattle()
	end
end

function GameController.ChessStageEnd()
	ChessUtil.Log("GameController ChessStageEnd")
	status = ChessConst.BattleStatusType.End

	-- todoChess 结束准备
end

-- todoChess 每一帧的倒计时、测试替换成按钮结束

function GameController.Destroy()
	GameController.UnregisterEvents()
end
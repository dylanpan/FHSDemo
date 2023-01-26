--config
require "afk/config/MCSharpImport"

-- Chess
require "Assets/Chess/Scripts/Lua/chessLogic/ChessRequires"

-- -- prefab system 替换
-- MHUpdateMgr = MHU3D.MHPrefabSysUpdateMgr

-- require "afk/config/MGameConst"
-- require "afk/config/MUIConfig"
-- require "afk/config/MResConfig"
-- require "afk/config/MMsgType"
-- require "afk/config/MEventType"
-- require "afk/config/MFuncVersionConfig"
-- require "afk.config.MSDKConst"

-- --common
-- require "afk/common/MCommonRequires"

-- --core
-- require "afk/core/PrefabSysGameMgr"
-- require "afk/core/MEventSystem"
-- require "afk/core/MMsgMgr"
-- require "afk/core/MCountdownMgr"
-- require "afk/core/MSceneMgr"
-- require "afk/core/MPopoutMgr"

-- --data
-- require "afk/data/MSystemBase"
-- require "afk/data/MLoginData"
-- require "afk/data/MGameData"
-- require "afk/data/MGlobalData"
-- require "afk/data/MTempData"
-- require "afk/data/MLocalStorageData"
-- require("afk.data.MSystemRequire")

-- --utils
-- require "afk/utils/MUtil"
-- require "afk/utils/MUIElementBinder"
-- MUIElementBinder = EB
-- require "afk/utils/MSequence"
-- require "afk/utils/MSequenceMgr"
-- require "afk/utils/MObjectPool"
-- require "afk/utils/MTemplatePool"
-- require "afk.utils.MGAUtil"
-- require "afk/utils/MLanguage"
-- -- require "afk.utils.MPayUtil"
-- -- require "afk.utils.MBIUtil"

-- --logic
-- -- require "afk/logic/hero/MHeroData"
-- -- require "afk/logic/campaign/MCampaignData"
-- -- require "afk/logic/task/MTaskData"
-- -- require "afk/logic/equip/MEquipData"
-- -- require "afk/logic/equip/MEquipArtifactData"
-- -- require "afk/logic/equip/MEquipExclusiveData"
-- -- require "afk.logic.temple.MTempleData"
-- -- require "afk.logic.social.MFriendsData"
-- require "afk/logic/reddot/MRedDotSystem"
-- require "afk/logic/reddot/MRedDotConfig"
-- require "afk/logic/reddot/MRedDotTree"
-- require "afk/logic/reddot/MRedDotNode"
-- -- require "afk/logic/mailbox/MMailData"
-- -- -- require "afk/logic/shop/MShopData"
-- -- require "afk.logic.pub.MPubData"
-- -- require "afk/logic/guide/MGuideMgr"
-- -- require "afk.logic.arena.MArenaData"
-- -- require "afk/logic/store/MStoreData"
-- -- require "afk/logic/gharry/MGharryData"
-- -- require "afk/logic/library/MLibraryData"
-- -- require "afk/logic/bounty/MBountyData"
-- -- require "afk/logic/guild/MGuildData"
-- -- require "afk/logic/resonant/MResonantData"
-- -- require "afk/logic/guildBoss/MGuildBossData"
-- -- require "afk.logic.monument.MMonumentData"
-- -- require "afk.logic.tower.MTowerData"
-- -- require "afk.logic.bag.MBagData"
-- -- --require "afk/logic/chat/MChatData"
-- -- require "afk/logic/chat/MChatDataNew"
-- -- require "afk/logic/chat/MChatCell"
-- -- require "afk/logic/chat/MChatCellInfo"
-- -- require "afk/logic/chat/MChatCellPool"
-- -- require "afk/logic/battle/MEmbattleData"
-- -- require "afk/logic/peaksoftime/MPeaksOfTimeRequires"
-- -- require "afk/logic/manor/MManorAndFieldBase"
-- -- require "afk/logic/maze/MMazeData"
-- -- require "afk/logic/campaign/MAfkBattleLogic"
-- require "afk/logic/battle/MBattleRecordPlayer"
-- -- require "afk/logic/activity/MActivityData"
-- -- require "afk.logic.peaksoftime.MPeaksOfTimeData"
-- -- require "afk.logic.social.MMercenaryData"
-- -- require "afk/logic/battle/MBattleRecordData"
-- -- require "afk.logic.wondad.MUIWondAdRequire"
-- -- require "afk.logic.battle.MStorylineMgr"
-- -- require "afk.logic.plotdialogue.MPlotDialogueItem"
-- -- require "afk.logic.battle.MRacialBonusData"


MGameMain = {
	clickFxResource = nil
}

function MGameMain:Init()
    
	-- todoChess
	GameController.Init()


	-- MGAUtil.Track(GAEventName.LoadLua)
	-- MGAUtil.Track(GAEventName.TextureSupportASTC, {[GAPropName.SupportASTCProp]=(MHUtil.IsSupportASTC() and 1 or 0)})

	-- --获取平台类型
	-- platform = MHUtil.GetPlatformType()
	-- --初始化UI配置
	-- MHUIMgr:InitConfig(UI_CONFIG)
	-- --初始化多语言
	-- MLanguage.Init()
	-- --预加载图集
	-- -- MHResMgr:PreloadAtlasAsync(AtlasConfig.AlwaysInclude)
	-- --初始化GameMgr
	-- MGameMgr:Init()
	-- --初始化声音
	-- MUtil.InitSoundVolume()
	
	-- --初始化性能等级
	-- -- MHGfxMgr:InitQulaityLevelByGetDeviceInfo();

	-- --初始化动骨屏蔽动作
	-- MHActorMgr:InitDynamicBoneSetting(DynaimcBoneForbidden)

	-- UpdateBeat:Add(self.Update,self)

	-- --默认关闭手机日志
	-- if platform == PlatformType.ANDROID or platform == PlatformType.IOS then
	-- 	MHOptimizeMgr.openDebug = false
	-- 	MHUtil.SetDebugLog(false)
	--     isDebug = false
	-- end

	-- if platform == PlatformType.ANDROID then
	-- 	MHUIMgr.isOpenUIAdaptive = false
	-- else
	-- 	MHUIMgr.isOpenUIAdaptive = true
	-- end
	
	-- OFFLINE_BATTLE = true
	-- PREFAB_SYSTEM = true
	-- -- 初始化数据
	-- if(PREFAB_SYSTEM)then
	-- 	--初始化一些环境参数参数
	-- 	MGameData.inst.userInfo = {}
	-- 	MGameData.inst.userInfo.chapterLevel = 1001
	-- 	MLoginData.inst.userId = 10001
	-- 	MFuncVersionConfig.versionType = MFuncVersionType.All
	-- 	MHGfxMgr:SetQualityLevel(QualityLevel.High)
	-- 	MFuncVersionConfig:Init()
	-- 	MGlobalData.inst.battleCameraParamType = MLocalStorageData:GetInt(LocalStorageKey.BattleCameraType, 1)
	-- 	showClickFx = false
	-- 	MBattleRecordPlayer.inst.isOfflineBattle = true
	-- 	MSystemBase:Init()
	-- end
end


function MGameMain:Update()
	-- if Input.GetMouseButtonDown(0) then
	-- 	if not showClickFx then
	-- 		return
	-- 	end
	-- 	if self.clickFxResource == nil then
	-- 		self.clickFxResource = ResourceUnit.Get(MHResType.FX, "com0__fx_ui_tongyong_dianji_fx", MHUIMgr.gameObject)
	-- 		self.clickFxResource:BeginLoad(nil)
	-- 		MHUIMgr:CreateUI(UI.Maze)
	-- 		return
	-- 	end
	-- 	if not self.clickFxResource.Loaded or not MGlobalData.inst.canvasScaleInited then
	-- 		return 
	-- 	end
	-- 	if self.clickFx == nil then
	-- 		MHUIMgr.clickFx.localScale = MGlobalData.inst.canvasScale
	-- 		self.clickFx = MHFxMgr:CreateFx("com0__fx_ui_tongyong_dianji_fx")
	-- 		self.clickFx:SetParent(MHUIMgr.clickFx)
	-- 		self.clickFx:SetLayer(Layer.UI)
	-- 		self.clickFx:SetOrderInLayer(1000)
	-- 	else
	-- 		if self.clickFx.transParent then
	-- 			MHUtil.SetActive(self.clickFx.transParent,false)
	-- 			MHUtil.SetActive(self.clickFx.transParent,true)
	-- 			self.clickFx.transParent.localPosition = MHUIMgr.uiCamera:ScreenToWorldPoint(Vector3.New(Input.mousePosition.x,Input.mousePosition.y,0))
	-- 		end
	-- 	end
	-- elseif Input.GetKeyDown(UnityEngine.KeyCode.Escape) then
	-- 	if platform == PlatformType.ANDROID or platform == PlatformType.EDITOR then
	-- 		local uiInst = MHUIMgr:GetTopUI()
	-- 		if uiInst then
	-- 			local uiLayer = uiInst.uiProperty.layer
	-- 			local uiName = uiInst.uiName
	-- 			print("-------------press BackKey: " .. uiName)
	-- 			if MGuideMgr.inst.hasTrigger and MGuideMgr.inst.guideType == MGuideMgr.GUIDE_TYPE.FORCE and uiName ~= UI.QuitGameWindow then 
	-- 				MUtil.ShowQuitGameConfirmWindow()
	-- 				return 
	-- 			end
	-- 			local inBattle = MGameBattleMgr.GetCurrBattle()
	-- 			if inBattle then
	-- 				-- 战斗中需要的继续或暂停
	-- 				MUtil.SendEvent(EVENT.GOOGLE_PLAY_RETURN_BATTLE); 
	-- 				if (uiName == UI.QuitGameWindow) then
	-- 					MHUIMgr:RemoveUI(uiName);
	-- 				end
	-- 			elseif uiLayer == UI_LAYER_TYPE.STACK then
	-- 				if (uiName == UI.Login 
	-- 						or uiName == UI.HUD
	-- 						or uiName == UI.Win 
	-- 						or uiName == UI.Lose
	-- 						or uiName == UI.PrivatePolicy
	-- 						or uiName == UI.PlotDialogue
	-- 						or uiName == UI.Pub) then
	-- 					MUtil.ShowQuitGameConfirmWindow()
	-- 				elseif uiName == UI.HeroInfo 
	-- 							or uiName == UI.UserSettings 
	-- 							or uiName == UI.UserSettingsName
	-- 							or uiName == UI.PeaksOfTimeMapView then
	-- 					-- 需要特殊处理且不关闭的界面需要监听的事件
	-- 					MUtil.SendEvent(EVENT.GOOGLE_PLAY_RETURN_NORMAL, uiName);
	-- 				else
	-- 					-- 需要特殊处理才能关闭的界面需要监听的事件
	-- 					MUtil.SendEvent(EVENT.GOOGLE_PLAY_RETURN_NORMAL, uiName);
	-- 					MHUIMgr:RemoveUI(uiName);
	-- 				end
	-- 			elseif uiLayer == UI_LAYER_TYPE.TOP then
	-- 				if (uiName == UI.QuitGameWindow
	-- 						or uiName == UI.SkillTips
	-- 						or uiName == UI.ConfirmWindow) then
	-- 					MHUIMgr:RemoveUI(uiName);
	-- 				else
	-- 					MUtil.ShowQuitGameConfirmWindow()
	-- 				end
	-- 			end
	-- 		end
	-- 	end
    -- end
	
end

MGameMain:Init()

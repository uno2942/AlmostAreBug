# GameManager

게임 전반을 관리하는 매니저 클래스

Type: Class

Derived from: Monobehavior

## Constants 



## Fields

private Parser parser

private BoardManager boardManager

private DialogWindow DialogWindow

private UiManager uiManager

private BugManager bugManager

private SceneManager sceneManager

private ItemManager itemManager

private Inventory inventory

각 Manager class에 접근하기 위한 Fields

--------



## Properties



## Methods

- void Start()



- void Update()

## Events



## Operators



# BoardManager

게임 전반의 맵과 플레이어/카메라의 위치를 관리하는 클래스

Type: Class

Derived from: Monobehavior

## Constants 



## Fields

private GameObject mainCamera

------



## Properties



## Methods

- void Start()



- void Update()

## Events



## Operators

# DialogWindow



## Constructor



## Constants 



## Fields

private DialogWindow dialogWindow

## Properties



## Methods

public void WriteALine( string str )

## Events



## Operators



# UiManager



## Constructor



## Constants 



## Fields

private DialogWindow dialogWindow

private HorizontalLayoutGroup itemPanel;

## Properties



## Methods

public void OpenMessageBox( ItemManager.ItemList item, ItemManager.PresentState presentState )

public void MoveLeft()

현재 Scene에 따라 왼쪽 Scene으로 이동

public void MoveRight()

현재 Scene에 따라 오른쪽 Scene으로 이동

## Events



## Operators



# BugManager



## Constructor



## Constants 



## Fields

[System.Flags]
public enum BugList 

Dictionary<BugList, bool> bugDic

​	버그를 통과할 때 마다 true를 대입한다.

## Properties



## Methods

public bool IsBugOvercomed(BugList bug)

public void BugOvercomed(BugList bug)

## Events



## Operators



# SceneManager



## Constructor



## Constants 



## Fields



## Properties



## Methods



## Events



## Operators



# ItemManager



## Constructor



## Constants 



## Fields

[System.Flags]
    public enum ItemList { Scissors };

[System.Flags]

public enum PresentState { Dropped, Gotten, Discarded};

## Properties



## Methods



## Events



## Operators



# Inventory



## Constructor



## Constants 

const int MAXITEMNUM = 20;

## Fields

public struct ItemElement { public ItemManager.ItemList item; public int num;}



private ItemElement[]  itemsInInventory

​	인벤토리 안의 아이템들을 가지고 있는 List



## Properties

public ItemElement[] ItemsInInventory { get => itemsInInventory; }

## Methods

public bool AddItem(ItemManager.ItemList itemList )

public bool RemoveItem( ItemManager.ItemList itemList )

public ItemElement CheckItemElement( ItemManager.ItemList itemList )

public bool CheckItem( ItemManager.ItemList itemList )

## Events



## Operators



# Item

게임에 사용되는 아이템들의 Base class. 게임 상의 Item GameObject의 Component로 붙는다. 

Type: Class

Derived from: Monobehavior

## Constructor



## Constants 



## Fields

public PresentState presentState;

​public ItemManager.ItemList item;

public event EventHandler Click

​	아이템이 클릭되었을 때 해야할 반응을 Event로 처리한다.(현재 생각은 Inventory로 들어갔을 때 EventHandler를 바꿔주는 방식으로 할까 생각 중)

public event ClickEventHandler ClickEvent;

private UiManager uiManager;

## Properties



## Methods

protected virtual void start()

protected virtual void update()

public virtual void Clicked()

public virtual void ItemGet(ItemManager.ItemList item, ItemManager.PresentState presentState)

## Events



## Operators





# Parser

외부 텍스트 파일을 읽어들이는 클래스(현재 생각은 JSON을 써서 data 관리를 하는 것입니다. 안 되면 txt나 csv로...)

Type: Class

## Constructor

public Parser()

## Constants 



## Fields



## Properties



## Methods



## Events



## Operators


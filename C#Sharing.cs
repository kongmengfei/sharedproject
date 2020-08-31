List list = clientContext.Web.Lists.GetByTitle("My test doc lib");       
clientContext.Load(list.RootFolder, x => x.ServerRelativePath);
clientContext.ExecuteQuery();
Console.WriteLine(list.RootFolder.ServerRelativePath);         
ListItemCollectionPosition itemPosition = null;

do{
  CamlQuery camlQuery = new CamlQuery
  {
    ListItemCollectionPosition = itemPosition,
    FolderServerRelativePath = list.RootFolder.ServerRelativePath,
    ViewXml = @"<View Scope='All'><Query><Where><Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>1</Value></Eq></Where></Query><RowLimit Paged='TRUE'>1</RowLimit></View>"
  };
    
  ListItemCollection listItems = list.GetItems(camlQuery);
  clientContext.Load(listItems);
  clientContext.ExecuteQuery();

  itemPosition = listItems.ListItemCollectionPosition;

  foreach (ListItem listItem in listItems)
    Console.WriteLine("Item Title: {0}", listItem.Id);
    
} while (itemPosition != null);

/*
  Hit 'ctrl + d' or 'cmd + d' to run the code, view console for results
*/
import { ClientsideWebpart, sp } from "@pnp/sp/presets/all";

(async () => {

  const web = await sp.web.select("Title")()
  console.log("Web Title: ", web.Title);

  // use from the sp.web fluent chain
  const page = await sp.web.loadClientsidePage("/sites/tcccc/SitePages/ssssss.aspx");

  let control = page.findControl(c => c['title'] == 'Document library') as ClientsideWebpart;

  console.log(control);

  control.setProperties(
    {
      hideCommandBar: true,
      isDocumentLibrary: true,
      selectedListId: "bb30a1a4-c5fd-4cc5-a8cc-b3c0163f5555",
      selectedListUrl: "/sites/tcccc/SiteAssets",
      selectedViewId: "1419b89f-e56f-4337-9028-3f33560c59fc",
      webRelativeListUrl: "/SiteAssets"
    }
  );

  control.data.webPartData.serverProcessedContent.searchablePlainTexts= {listTitle: "Site Assets"};
  

  page.save();
  console.log(control);

})().catch(console.log)




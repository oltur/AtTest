﻿import * as React from 'react'
import * as Navigator from "@framework/Navigator"
import * as DashboardClient from "@extensions/Dashboard/DashboardClient"
import * as AuthClient from '@extensions/Authorization/AuthClient'
import { useAPI } from '../../Framework/Signum.React/Scripts/Hooks';

export default function Home() {

  var [loaded, setLoaded] = React.useState(false);
  React.useEffect(() => {

    if (!AuthClient.currentUser()) {
      Navigator.history.push("~/publicCatalog");
    }//PublicCatalog

    if (AuthClient.currentUser()) {
      DashboardClient.API.home()
        .then(h => {
          if (h)
            Navigator.history.push(`~/dashboard/${h.id}`);
          else
            setLoaded(true);
        });
    }
    else //Dashboard
      setLoaded(true);

  }, []);




  if (!loaded)
    return null;

  return (
    <div>
      <br />
      <div className="jumbotron">
        <h1 className="display-4">Welcome to Signum React</h1>
        <br />
        <p className="lead">AtTest is a demo application from <a href="http://www.signumsoftware.com" title="Signum Software">Signum Software</a> based on Northwind database from Microsoft:</p>
        <p>
          To learn more about Signum Framework visit <a href="http://www.signumframework.com" title="Signum Framework">http://www.signumframework.com</a>.
          </p>
        <p> To be effective in Signum React you will also need to know:</p>
        <ul>
          <li><a href="http://www.typescriptlang.org/" title="Typescript">Typescript</a></li>
          <li><a href="https://facebook.github.io/react">React</a></li>
          <li><a href="http://getbootstrap.com/" title="Bootstrap">Bootstrap</a></li>
          <li><a href="http://webpack.com/" title="Webpack">Webpack</a></li>
        </ul>
      </div>
    </div>
  );
}

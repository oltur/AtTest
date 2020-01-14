﻿import * as React from 'react'
import { AddressEmbedded } from '../AtTest.Entities'
import { ValueLine, EntityLine, EntityCombo, EntityList, EntityDetail, EntityStrip, EntityRepeater, TypeContext } from '@framework/Lines'

export default function Address(p : { ctx: TypeContext<AddressEmbedded> }){
  const ctx = p.ctx.subCtx({ formGroupStyle: "SrOnly", placeholderLabels: true });
  return (
    <div>
      <ValueLine ctx={ctx.subCtx(a => a.address)} />
      <div className="row">
        <div className="col-sm-6"><ValueLine ctx={ctx.subCtx(a => a.city)} /></div>
        <div className="col-sm-6"><ValueLine ctx={ctx.subCtx(a => a.region)} /></div>
      </div>
      <div className="row">
        <div className="col-sm-6"><ValueLine ctx={ctx.subCtx(a => a.postalCode)} /></div>
        <div className="col-sm-6"><ValueLine ctx={ctx.subCtx(a => a.country)} /></div>
      </div>
    </div>
  );
}

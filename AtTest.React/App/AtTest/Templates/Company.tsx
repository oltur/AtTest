﻿import * as React from 'react'
import { CompanyEntity, OrderEntity } from '../AtTest.Entities'
import { ValueLine, TypeContext } from '@framework/Lines'
import { SearchControl } from '@framework/Search';

export default function Company(p : { ctx: TypeContext<CompanyEntity> }){
  const ctx = p.ctx;
  return (
    <div>
      <ValueLine ctx={ctx.subCtx(c => c.companyName)} />
      <ValueLine ctx={ctx.subCtx(c => c.contactName)} />
      <ValueLine ctx={ctx.subCtx(c => c.contactTitle)} />
      <h2>{OrderEntity.nicePluralName()}</h2>
      <SearchControl findOptions={{
        queryName: OrderEntity,
        parentToken: "Customer",
        parentValue: ctx.value
      }} showSimpleFilterBuilder={false} />
    </div>
  );
}

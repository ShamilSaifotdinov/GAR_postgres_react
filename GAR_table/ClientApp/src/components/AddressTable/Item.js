import React, { useState } from 'react'
import { AddressTable } from './Table'
import { ObjectInfo } from './ObjectInfo'

export const Item = ({ address, type, region }) => {
  const [openInfo, setInfo] = useState(false);
  const [open, setOpen] = useState(false)

  return <>
    <tr>
        <td onClick={() => setOpen(!open)} style={{ width: "32.8px" }}><i className={open ? "bi bi-chevron-down" : "bi bi-chevron-right"}></i></td>
        <td onClick={() => setInfo(true)}>{address.id}</td>
        <td onClick={() => setInfo(true)}>{address.name}</td>
        <td onClick={() => setInfo(true)}>{address.typeName}</td>
        <td onClick={() => setInfo(true)}>{address.code}</td>
        <td onClick={() => setInfo(true)}>{address.okato}</td>
        <td onClick={() => setInfo(true)}>{address.oktmo}</td>
        <td onClick={() => setInfo(true)} className='tb-last-col pr-0'>{address.level}</td>
      {openInfo && <ObjectInfo isModal={openInfo} setModal={setInfo} regionId={region} objectId={address.id} />}
    </tr>
    {
      open &&
      <tr>
        <td></td>
        <td colSpan={7} className='pr-0'>
          <AddressTable type={type} id={address.id} region={region} />
        </td>
      </tr>
    }
  </>
}
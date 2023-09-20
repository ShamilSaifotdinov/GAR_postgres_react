import React, { useEffect, useState } from 'react'
import { Item } from '../AddressTable/Item';
import { useAddresses } from '../../hooks/address.hook';

export const AddressTable = ({ id, type, region }) => {
  const [childs, setChilds] = useState([])
  const [isLoading, getAddresses] = useAddresses()


  useEffect(() => {
    if (!childs.length) {
      getAddresses(`${type}Test?region=${region}&id=${id}`)
        .then((data) => {
          data.sort((a, b) => a.code - b.code)
          setChilds(data)
        })
      }
      // getAddresses(`${type}Test2?region=${region}&id=${id}`)
      //   .then((data) => console.log(data))
  }, [])

  return (
    isLoading
      ? "Загрузка..."
      : (childs.length ?
        <table className="table table-striped table-sm mb-0 p-0" aria-labelledby="tableLabel">
          <thead>
            <tr>
              <th></th>
              <th className="col-md-1">ID</th>
              <th>Наименование</th>
              <th className="col-md-1">Тип</th>
              <th>КЛАДР</th>
              <th>ОКАТО</th>
              <th>ОКТМО</th>
              <th className='tb-last-col'>Уровень</th>
            </tr>
          </thead>
          <tbody>
            {
              childs.map(address => <Item key={address.id} address={address} type={type} region={region} />)
            }
          </tbody>
        </table>
        : "Дочернии элементы отсутствуют")
  );
}

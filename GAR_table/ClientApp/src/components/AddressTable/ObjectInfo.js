import React, { useState, useEffect, useMemo, useRef } from 'react'
import { Modal } from '../../hooks/modal.hook'
import { useAddresses } from '../../hooks/address.hook'
import { MapContainer, TileLayer, useMap, GeoJSON, Rectangle } from 'react-leaflet'
import './ObjectInfo.css'
import 'leaflet/dist/leaflet.css';

const innerBounds = [
    [49.505, -2.09],
    [53.505, 2.09],
]
const outerBounds = [
    [50.505, -29.09],
    [52.505, 29.09],
]

const redColor = { color: 'red' }
const whiteColor = { color: 'white' }

function SetBoundsRectangles({data}) {
    const [bounds, setBounds] = useState(outerBounds)
    const map = useMap()
    console.log(data)
    console.log(data.getBounds())

    const innerHandlers = useMemo(
        () => ({
            click() {
                setBounds(innerBounds)
                map.fitBounds(innerBounds)
            },
        }),
        [map],
    )
    const outerHandlers = useMemo(
        () => ({
            click() {
                setBounds(outerBounds)
                map.fitBounds(outerBounds)
            },
        }),
        [map],
    )

    return (
        <>
            <Rectangle
                bounds={outerBounds}
                eventHandlers={outerHandlers}
                pathOptions={bounds === outerBounds ? redColor : whiteColor}
            />
            <Rectangle
                bounds={innerBounds}
                eventHandlers={innerHandlers}
                pathOptions={bounds === innerBounds ? redColor : whiteColor}
            />
        </>
    )
}

export const ObjectInfo = ({ isModal, setModal, regionId, objectId }) => {
    const [geom, setGeom] = useState({})
    const [info, setInfo] = useState([])
    const [isLoading, getAddresses] = useAddresses()
    const region = useRef(null)

    useEffect(() => {
        getAddresses(`infoTest?region=${regionId}&id=${objectId}`)
            // .then((data) => console.log(data))
            .then((data) => {
                setInfo(data.param_info)
                data.geom && setGeom(JSON.parse(data.geom))
            })
    }, [])

    return (
        <Modal
            isVisible={isModal}
            title={objectId}
            content={
                !isLoading && <div className='modal-container'>
                    <table className="table table-striped info-table" aria-labelledby="tableLabel">
                        <tbody>
                            {
                                info.length
                                    ? info.map((item) =>
                                        <tr key={item.id}>
                                            <td>{item.name}</td>
                                            <td>{item.value}</td>
                                        </tr>)
                                    : <tr colSpan={2}><td>Данные отсутствует</td></tr>
                            }
                        </tbody>
                    </table>
                    {Object.keys(geom).length > 0 &&
                        <MapContainer center={[60, 110]} zoom={2} className='info-map'>
                            {
                                //center={[51.505, -0.09]} zoom={13} bounds={geom}
                            }
                            <GeoJSON ref={region} data={geom} />
                            <TileLayer
                                attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                            />
                            {/* <SetBoundsRectangles data={region}/> */}
                        </MapContainer>
                    }
                </div>
            }
            footer={<button type="button" className="btn btn-primary" onClick={() => setModal(false)}>Закрыть</button>}
            onClose={() => setModal(false)}
        />
    )
}
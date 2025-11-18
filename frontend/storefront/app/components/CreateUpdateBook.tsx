import {BookRequest} from "@/app/services/books";
import Input from "antd/es/input/Input";
import TextArea from "antd/es/input/TextArea";
import Modal from "antd/es/modal/Modal";
import {useEffect, useState} from "react";

interface Props {
    mode: Mode;
    values: Book;
    isModalOpen: boolean;
    handleCancel: () => void;
    handleCreate: (request: BookRequest) => void;
    handleUpdate: (id: string, request: BookRequest) => void; 
}

export enum Mode {
    Create,
    Edit,
}

export const CreateUpdateBook = ({
    mode,
    values, 
    isModalOpen,
    handleCancel,
    handleCreate,
    handleUpdate,
}: Props) => {
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [price, setPrice] = useState<number>(0);
    
    useEffect(() => {
        setTitle(values.title)
        setDescription(values.description)
        setPrice(values.price)
    })
    
    const handleOnOk = async () => {
        const bookRequest = { title, description, price};
        
        mode == Mode.Create
            ? handleCreate(bookRequest)
            : handleUpdate(values.id, bookRequest)
    }
    
    return (
        <Modal title={mode === Mode.Create
            ? "Add Book"
            : "Edit Book"
            } 
            open={isModalOpen}
            cancelText="Cancel">
            onOk={handleOnOk}
            onCancel={handleCancel}
            <div className="book__modal">
                <Input
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    placeholder="Name" 
                />
                <TextArea
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    placeholder="Description"
                    autoSize={{ minRows: 3, maxRows: 3 }}
                />
                <Input
                    value={price}
                    onChange={(e) => setTitle(e.target.value)}
                    placeholder="Price"
                />
            </div>
        </Modal>
    )
}